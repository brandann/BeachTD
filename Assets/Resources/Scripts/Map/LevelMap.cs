using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMap {

	#region private members
	private bool[,] 	_map;		// bool array of tower accessable objects
	private Vector3[] 	_waypoints;	// enemys path for map
	private int			_hitpoints;	// "lives" for level
	private float		_scale;		// level scale
	protected int 		_hitpointsmax;
	protected const bool I = true;	// for ease of reading
	protected const bool O = false;	// for ease of reading
	protected const int P = -1; 	// pathtile
	protected const int T = -2;		// tower tile
	protected const int S = 0; 		// staring waypoint position
	protected const int E = int.MaxValue; //ending waypoint position
	#endregion
	
	#region public accessors
	public bool[,] Map
	{
		get{ return _map; }
		set{ _map = value; }
	}
	
	public Vector3[] Waypoints
	{
		get{ return _waypoints; }
		set{ _waypoints = value; }
	}
	
	public int HitPoints
	{
		get{ return _hitpoints; }
		set{ _hitpoints = value; }
	}
	
	public float Scale
	{
		get{ return _scale; }
		set{ _scale = value; }
	}
	
	public int HIT_POINTS_MAX
	{
		get{ return _hitpointsmax; }
	}
	
	// SetMap
	// turns a int[][] map into a bool map with waypoints
	public void SetMap(int[,] m)
	{
		bool[,] tempmap = new bool[m.GetLength(0), m.GetLength(1)];
		Vector3 StartPos, EndPos;
		Dictionary<int, Vector3> DictWayPoints = new Dictionary<int, Vector3>();
		for(int i = 0; i < tempmap.GetLength(0); i++)
		{
			for(int j = 0; j < tempmap.GetLength(1); j++)
			{
				int token = m[i,j];
				if (token == T)
				{
					tempmap[i,j] = true;
				}
				else if (token == P)
				{
					tempmap[i,j] = false;
				}
				else if(token >= 0)
				{
					int index = 0;
					if(token == S)
					{
						index = 0;
					}
					else if(token == E)
					{
						index = int.MaxValue;
					}
					else
					{
						index = m[i,j];
					}
					DictWayPoints.Add(index, new Vector3(i, j));
					tempmap[i,j] = false;
				}
				
			}
		}
	}
	#endregion
	
	#region Private Methods

	#endregion
	
}
