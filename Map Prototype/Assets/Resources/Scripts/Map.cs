using UnityEngine;
using System.Collections;

public class Map {

	bool[,] mMap;
	Vector2[] mWaypoints;
	
	public void setMap(bool[,] map, Vector2[] waypoints)
	{
		mMap = map;
		mWaypoints = waypoints;
	}
	
	public bool[,] getMap()
	{
		return mMap;
	}
	
}
