using UnityEngine;
using System.Collections;

public class Map {

	// map layout buildable spots
	bool[,] mMap;
	
	// enemy pathing points
	Vector3[] mWaypoints;
	
	int hitsMax = 0;
	
	// sets the map information
	// map is the boolean array of buildable spaces
	// waypoints are the enemys pathing points
	public void setMap(bool[,] map, Vector3[] waypoints, int hm)
	{
		mMap = map;
		mWaypoints = waypoints;
		hitsMax = hm;
	}
	
	// returns the boolean array for the map
	public bool[,] getMap()
	{
		return mMap;
	}
	
	// returns the vector2 array for the waypoints
	public Vector3[] getWaypoints()
	{
		return mWaypoints;
	}
	
	public int getHitsMax()
	{
		return hitsMax;
	}
	
}
