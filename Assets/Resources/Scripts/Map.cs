using UnityEngine;
using System.Collections;

public class Map {

	// map layout buildable spots
	bool[,] mMap;
	
	// enemy pathing points
	Vector2[] mWaypoints;
	
	// sets the map information
	// map is the boolean array of buildable spaces
	// waypoints are the enemys pathing points
	public void setMap(bool[,] map, Vector2[] waypoints)
	{
		mMap = map;
		mWaypoints = waypoints;
	}
	
	// returns the boolean array for the map
	public bool[,] getMap()
	{
		return mMap;
	}
	
	// returns the vector2 array for the waypoints
	public Vector2[] getWaypoints()
	{
		return mWaypoints;
	}
	
}
