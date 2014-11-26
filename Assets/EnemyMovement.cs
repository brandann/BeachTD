using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Vector2[] waypoints;

	// Use this for initialization
	void Start () {
	
		Mapmanager mm = GameObject.Find("MapManager").GetComponent<Mapmanager>();
		waypoints = mm.getWaypoints();
		Vector3 firstposition = new Vector3(waypoints[0].x, waypoints[0].y, 0);
		firstposition *= mm.getScale();
		this.transform.position = firstposition;		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
/*

using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {
	
	public GameObject[] waypoints;
	
	private Vector3 nextPoint; // the next waypoint the enemy is traveling to
	private float speed = 2;
	private int listPos = 1; //current index of the array list
	bool right = false; //should the enemy rotate right?
	bool left = false; //should the enemy rotate left?
	
	private ArrayList waypointList;
	
	
	void Start ()
	{
		
		waypointList = new ArrayList (); 
		waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //unsorted array of waypoints
		
		if (waypoints != null) //at least one waypoint in the array
		{ 
			sortWaypoints ();
			transform.position = (Vector3)waypointList [0]; //starts at origin
			nextPoint = (Vector3)waypointList [1]; //automatically queues up the first waypoint in the list
			transform.up = nextPoint - transform.position; //gets the first direction
		}
	}
	
	void Update ()
	{
		float dot = 0;
		Vector3 currentPos = nextPoint - transform.position; //how close am I to the next waypoint? 
		
		if (currentPos != transform.up)// no need to rotate, object is moving in a straight line
		{
			dot = transform.up.x * -currentPos.y + transform.up.y * currentPos.x; //how much more do I have to rotate? 
		}
		
		//once enemy gets really close to the waypoint,
		// go to the next waypoint until there are none left, then reverse
		if (currentPos.magnitude < .5 && listPos < waypointList.Count - 1)
		{
			listPos++;
			nextPoint = (Vector3)waypointList [listPos];
			Vector3 dir = nextPoint - transform.position; // the new vector to turn towards
			
			// by making dir.y negative we flip the angle 90 degrees. By doing that we can easily use to dot product to determine
			// right or left. Normally, the dot product returns a - or + angle based on front or behind
			dot = transform.up.x * -dir.y + transform.up.y * dir.x;
			
			if (dot > 0) //my next waypoint is to the right
			{
				print ("b on the right of a");
				right = true;
			}
			else if (dot < 0) // my next waypoint is to the left
			{
				left = true;
				print ("b on the left of a");
			}
			else
				print ("b parallel/antiparallel to a");
			
		} 
		else if (listPos == waypointList.Count - 1) //I'm out of waypoints, turn around
		{
			listPos = 0;
			waypointList.Reverse ();
		}
		
		
		
		if (right && dot < 0) { //I want to turn right as long as the dot calculation doesn't go over zero (which means the object has 
			// rotated past the waypoint it needs to go to)
			right = false;
			transform.up = currentPos; //makes sure we dont rotate past our destination 
		} else if (right) {
			transform.Rotate (Vector3.forward, -1f * (120f * Time.smoothDeltaTime));
		}
		
		
		if (left && dot > 0) { //exactly the same as above, but inverted
			left = false;
			transform.up = currentPos;
		} else if(left) {
			transform.Rotate (Vector3.forward, 1f * (120f * Time.smoothDeltaTime));	
		}
		
		//movement operation
		transform.position += (speed * Time.smoothDeltaTime) * transform.up;
		
	}
	//----------------------------------------------------------------------
	// similar to insertion sort. The method sorts the vectors based on the shortest distance from the current waypoint
	// once it finds the sortest distance it removes it from the array, and adds it to the arraylist in sorted order
	private void sortWaypoints()
	{
		
		
		Vector3 current = Vector3.zero; 
		waypointList.Add(current);
		
		for(int i = 0; i < waypoints.Length; i++) 
		{
			
			float temp = float.MaxValue; //the current shortest magnitude between vectors
			Vector3 tempV = new Vector3(); //the waypoint with the current shortest distance from "current"
			int currentIndex = 0; // the index of that waypoint, in order to remove it from the array
			
			for(int j = 0; j < waypoints.Length; j++)
			{
				if(waypoints[j] != null) //check all the non null array indexes
				{
					Vector3 distance = new Vector3(current.x - waypoints[j].transform.position.x,
					                               current.y - waypoints[j].transform.position.y, 0f);
					float dis = distance.magnitude;
					
					if(temp > dis) // found a shorter distance in the remaining waypoint array
					{
						
						tempV = waypoints[j].transform.position;
						temp = dis;
						currentIndex = j;
					}
					
				}
			}
			
			waypointList.Add(tempV);
			current = tempV;
			waypoints[currentIndex] = null;
			
		}
		
	}
	
	
}*/
