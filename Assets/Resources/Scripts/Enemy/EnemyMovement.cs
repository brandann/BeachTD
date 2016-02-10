using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	#region Public Members
	public enum EnemyMovementSpeed {Paused = 0, Slow = 1, Normal = 2, Fast = 3}
	public float DistanceTraveled { get; protected set; }
	public float speed;
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
	private float DistanceFromWaypoint;
	private float mEggSpeedMod = 1;
	#endregion
	
	#region Unity
	// Use this for initialization
	void Start () {
		DistanceFromWaypoint = speed * 0.02f;
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
            GetComponent<Enemy>().ChangeColor(Enemy.ColorState.Normal);
		}
		
		//Speed mods from tower
		SpeedMod = SpeedMods[(int) CurrentMovement];
		
		Vector3 currentPos = nextPoint - transform.position;
		if(currentPos.magnitude < DistanceFromWaypoint)
		{
			SetCurrentWaypoint(direction);
		}
		
		Vector3 moveDelta = (speed * SpeedMod * mEggSpeedMod * Time.smoothDeltaTime) * transform.up;
		transform.position += moveDelta;
		DistanceTraveled += moveDelta.magnitude;
		
	}


	#endregion
	
	#region Private Methods
	private void SetCurrentWaypoint(int deltaIndex)
	{
		ResetPosition();
		listPos += deltaIndex;
		if(listPos == -1)
		{
            GetComponent<Enemy>().RemoveAfterReachedStart();
			return; // keeps from getting an error thrown
		}
		else if(listPos >= waypoints.Length)
		{			
			// enemy is at the end of the track. 
			// check to see if any jewels are available buy not picked up
			EggManager eggManager = global.eggManager;
			if(eggManager.iseggAvail())
			{
				// TODO: try to pick up an egg
			}
			
			direction = -1;
			listPos -= 2;
			transform.Rotate (Vector3.forward, 180);
		}
		nextPoint = waypoints[listPos];
		transform.up = nextPoint - transform.position;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
	}
	
	private void ResetPosition()
	{
		this.transform.position = nextPoint;
	}
	#endregion
	
	#region Public Methods
	/// <summary>
	/// Modify the speed of enemy
	/// </summary>
	/// <param name="mod">Type of modification to be applied</param>
	/// <param name="duration">Lenght in seconds the modification should last</param>
	public void UpdateSpeedMod(EnemyMovementSpeed mod, float duration)
	{
		if(mod == EnemyMovementSpeed.Slow)
        {
            GetComponent<Enemy>().ChangeColor(Enemy.ColorState.Slow);
        }
		
		CurrentMovement = mod;
		SpeedMod = SpeedMods[(int) CurrentMovement];
		endModificationTime = Time.time + duration;
	}
	
	public void ReverseDirection()
	{
		if(direction > 0)
		{
			direction = -1;
			listPos += direction;
			transform.Rotate (Vector3.forward, 180);
			nextPoint = waypoints[listPos];
			transform.up = nextPoint - transform.position;
		}
	}
	
	public void SetEggSpeedMod()
	{
		mEggSpeedMod = 1.5f;
	}

	#endregion
}	