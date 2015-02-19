using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public enum EnemyMovementSpeed {Paused = 0, Slow = 1, Normal = 2, Fast = 3}
	public EnemyMovementSpeed CurrentMovement;
	private float[] SpeedMods = {0f, .5f, 1f, 2f};
	private float SpeedMod;
    public float DistanceTraveled { get; protected set; }

	private Vector3[] waypoints;
	private Vector3 nextPoint; // the next waypoint the enemy is traveling to
    
    //Time.time when the current modification should be removed and normal speed should be set
    private float endModificationTime;
    
	Global global;

	public int direction = 1;

    public int listPos{get; private set;} //current index of the array list
	private float speed = .5f; // enemy speed
	public float distance;// = .1f; //distance enemy must be away from waypoint to got to next
	public float rotateangle;// = 3; // rotation factor
	
	bool right = false; //should the enemy rotate right?
	bool left = false; //should the enemy rotate left?
	//private ArrayList waypointList;

	// Use this for initialization
	void Start () {
	
		CurrentMovement = EnemyMovementSpeed.Normal;
		SpeedMod = SpeedMods[(int) CurrentMovement];
	
		global = GameObject.Find("Global").GetComponent<Global>();
		waypoints = new Vector3[global.CurrentMap.Waypoints.Length];
		//Vector3 MoveOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
		Vector3 MoveOffset = Vector3.zero;
		Vector3[] Mapwaypoints = global.CurrentMap.Waypoints;
		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints[i] = MoveOffset + Mapwaypoints[i];
		}
		
		
		Vector3 firstposition = waypoints[0];
		//firstposition *= mm.getScale();
		///firstposition.x += mm.getScale()/2;
		//firstposition.y += mm.getScale()/2;
		this.transform.position = firstposition;		
		//waypointList = new ArrayList ();
		nextPoint = waypoints[1];
		transform.up = nextPoint - transform.position; //gets the first direction
		
        listPos = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //Remove modification after it has expired
        if (CurrentMovement != EnemyMovementSpeed.Normal && Time.time >= endModificationTime)
        {
            CurrentMovement = EnemyMovementSpeed.Normal;
            gameObject.GetComponent<Enemy>().ResetColor();
        }
	
		SpeedMod = SpeedMods[(int) CurrentMovement];
	
		if(Global.CurrentGameState != Global.GameState.Game)
		{
			return; // early out for not in playmode
		}

		float dot = 0;
		Vector3 currentPos = nextPoint - transform.position; //how close am I to the next waypoint? 
		
		if (currentPos != transform.up)// no need to rotate, object is moving in a straight line
		{
			dot = transform.up.x * -currentPos.y + transform.up.y * currentPos.x; //how much more do I have to rotate? 
		}
		
		//once enemy gets really close to the waypoint,
		// go to the next waypoint until there are none left, then reverse
		if (currentPos.magnitude < distance && listPos < waypoints.GetLength(0))
		{
			listPos += direction;
			if(listPos == waypoints.GetLength(0))
			{
				if(direction > 0)
				{
					ReverseWaypoints();
					GetComponent<Enemy>().PickUpEgg();
					listPos--;
				}
				else
				{
					
				}
			}
			if(listPos == -1)
			{
				AtGoal();
			}
			else
			{
				nextPoint = waypoints[listPos];
			}
			
			Vector3 dir = nextPoint - transform.position; // the new vector to turn towards
			
			// by making dir.y negative we flip the angle 90 degrees. By doing that we can easily use to dot product to determine
			// right or left. Normally, the dot product returns a - or + angle based on front or behind
			dot = transform.up.x * -dir.y + transform.up.y * dir.x;
			
			if (dot > 0) //my next waypoint is to the right
			{
				//print ("b on the right of a");
				right = true;
			}
			else if (dot < 0) // my next waypoint is to the left
			{
				left = true;
				//print ("b on the left of a");
			}
			//else
				//print ("b parallel/antiparallel to a");
			
		} 
		else if (listPos == waypoints.GetLength(0)) //I'm out of waypoints, turn around
		{
//			listPos = 0;
//			waypointList.Reverse ();
		}

		if (right && dot < 0) { //I want to turn right as long as the dot calculation doesn't go over zero (which means the object has 
			// rotated past the waypoint it needs to go to)
			right = false;
			transform.up = currentPos; //makes sure we dont rotate past our destination 
		} else if (right) {
			transform.Rotate (Vector3.forward, -rotateangle * (120f * Time.smoothDeltaTime));
		}
		
		
		if (left && dot > 0) { //exactly the same as above, but inverted
			left = false;
			transform.up = currentPos;
		} else if(left) {
			transform.Rotate (Vector3.forward, rotateangle * (120f * Time.smoothDeltaTime));	
		}
		
		//movement operation
        Vector3 moveDelta = (speed * SpeedMod * Time.smoothDeltaTime) * transform.up;
        transform.position += moveDelta;
        DistanceTraveled += moveDelta.magnitude;
	}
	
	public void ReverseWaypoints()
	{
		direction *= -1;
	}
	
	public void ReverseDirection()
	{
		if(direction > 0)
		{
			direction *= -1;
			listPos -= 1;
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
		Color currentColor = this.renderer.material.color;
		if(currentColor == Color.white)
		{
			this.renderer.material.color = Color.blue;
		}
		else if(currentColor == Color.red)
		{
			this.renderer.material.color = new Color(125/255, 50/255, 180/255);
		}
		
		CurrentMovement = mod;
		SpeedMod = SpeedMods[(int) CurrentMovement];
        endModificationTime = Time.time + duration;
	}
	
	private void AtGoal()
	{
		GetComponent<Enemy>().AtGoal();
		Destroy(this.gameObject);
	}
}