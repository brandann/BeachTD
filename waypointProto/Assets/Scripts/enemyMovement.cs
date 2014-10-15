using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public GameObject[] waypoints;

	private Vector3 nextPoint;
	private Vector3 currentPoint;
	private float speed = 3;
	private int listPos = 1; //current index of the array list

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
	
	// Update is called once per frame
	void Update ()
	{

		Vector3 currentPos = nextPoint - transform.position;

		//once enemy gets really close to the waypoint,
		// go to the next waypoint until there are none left, then reverse
		if (currentPos.magnitude < .05 && listPos < waypointList.Count-1) 
		{
		listPos++;
			nextPoint = (Vector3)waypointList [listPos];
			Vector3 dir = nextPoint - transform.position;

			transform.up = dir;
		} 
		else if (listPos == waypointList.Count-1)
		{
			waypointList.Reverse();
			listPos = 0;
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
				 int currentIndex = 0; // the index of that waypoint in order to remove it from the array

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

//			foreach(Vector3 v in waypointList)
//			{
//				print("count" + waypointList.Count);
//				print (v);
//			}
		}


}
