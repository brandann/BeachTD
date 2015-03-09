using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	#region Public Members
	public enum EnemyMovementSpeed {Paused = 0, Slow = 1, Normal = 2, Fast = 3}
	public float DistanceTraveled { get; protected set; }

	#endregion
	
	#region Private members
	private EnemyMovementSpeed CurrentMovement;
	private Global global;
	private Vector3[] waypoints;
	private Vector3 nextPoint; // the next waypoint the enemy is traveling to
	private int listPos;
	private int direction = 1;
	private float[] SpeedMods = {0f, .5f, 1f, 2f};
	private float SpeedMod;
	private float endModificationTime;

	private float speed = 1;
	
	
	//Time.time when the current modification should be removed and normal speed should be set
	
	
	
	bool right = false; //should the enemy rotate right?
	bool left = false; //should the enemy rotate left?
	#endregion
	
	#region Public Methods
	public void ReverseWaypoints()
	{
		direction *= -1;
	}
	
	public void ReverseDirection()
	{
		if(direction > 0)
		{
			ReverseWaypoints();
			listPos--;
			nextPoint = waypoints[listPos];
		}
		
	}
	
	/// <summary>
	/// Modify the speed of enemy
	/// </summary>
	/// <param name="mod">Type of modification to be applied</param>
	/// <param name="duration">Lenght in seconds the modification should last</param>
	public void UpdateSpeedMod(EnemyMovementSpeed mod, float duration)
	{
		Color currentColor = this.GetComponent<Renderer>().material.color;
		if(currentColor == Color.white)
		{
			this.GetComponent<Renderer>().material.color = Color.blue;
		}
		else if(currentColor == Color.red)
		{
			this.GetComponent<Renderer>().material.color = new Color(125/255, 50/255, 180/255);
		}
		
		CurrentMovement = mod;
		SpeedMod = SpeedMods[(int) CurrentMovement];
		endModificationTime = Time.time + duration;
	}
	#endregion
	
	#region Private Methods
	private void AtGoal()
	{
		GetComponent<Enemy>().AtGoal();
		Destroy(this.gameObject);
	}

	#endregion
	
	#region Unity
	// Use this for initialization
	void Start () {
		global = GameObject.Find("Global").GetComponent<Global>();
		CurrentMovement = EnemyMovementSpeed.Normal;
		SpeedMod = SpeedMods[(int) CurrentMovement];
		listPos = direction = 1;
		waypoints = global.CurrentMap.Waypoints;
		nextPoint = waypoints[listPos];
		transform.up = nextPoint - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(waypoints == null || Global.CurrentGameState != Global.GameState.Game)
		{
			return;
		}
		
		//Remove modification after it has expired
		if (CurrentMovement != EnemyMovementSpeed.Normal && Time.time >= endModificationTime)
		{
			CurrentMovement = EnemyMovementSpeed.Normal;
			gameObject.GetComponent<Enemy>().ResetColor();
		}
		
		//Speed mods from tower
		SpeedMod = SpeedMods[(int) CurrentMovement];
		
		Vector3 currentPos = nextPoint - transform.position;
		if(currentPos.magnitude < .1f)
		{
			listPos += direction;
			if(listPos == -1)
			{
				GetComponent<Enemy>().AtGoal();
				Destroy(this.gameObject);
				return; // keeps from getting an error thrown
			}
			if(listPos >= waypoints.Length)
			{
				GetComponent<Enemy>().ApplyEggToCrab(EggManager.EggLocations.End);
				direction = -1;
				listPos -= 2;
				transform.Rotate (Vector3.forward, 180);
			}
			nextPoint = waypoints[listPos];
		}
		
		transform.up = nextPoint - transform.position;
		Vector3 moveDelta = (speed * SpeedMod * Time.smoothDeltaTime) * transform.up;
		transform.position += moveDelta;
		DistanceTraveled += moveDelta.magnitude;
		
	}
	#endregion	

}	
