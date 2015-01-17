using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {

	#region Public Methods
	public void SetMap(int[,] m) 
	{
		_mapi = FixMap(m);
		_waypoints = FindWayPoints(_mapi);
		_mapb = IntMapToBool(_mapi);
	}
	#endregion
	
	#region public accessors
	public bool[,] BoolMap
	{
		get{ return _mapb; }
	}
	
	public int[,] IntMap
	{
		get{ return _mapi; }
	}
	
	public Vector3[] Waypoints
	{
		get{ return _waypoints; }
	}
	
	public int HitPoints
	{
		get{ return _hitpoints; }
	}
	
	public int HIT_POINTS_MAX
	{
		get{ return _hitpointsmax; }
	}
	
	public float Height
	{
		get { return (float) _mapi.GetLength(0); }
	}
	
	public float Width
	{
		get { return (float) _mapi.GetLength(1); }
	}
	#endregion
	
	#region private members
	protected int[,]		_mapi;		// int array of map w/ paths
	protected bool[,] 	_mapb;		// bool array of tower accessable objects
	protected Vector3[] 	_waypoints;	// enemys path for map
	protected int			_hitpoints;	// "lives" for level
	protected int 		_hitpointsmax;
	#endregion
	
	#region Private Methods
	private Vector3[] FindWayPoints(int[,] m)
	{
		int MaxWaypoint = 0;
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] > MaxWaypoint)
				{
					MaxWaypoint = m[i,j];
				}
			}
		}
		Vector3[] FoundWaypoints = new Vector3[MaxWaypoint+1];
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] >= 0)
				{
					FoundWaypoints[m[i,j]] = new Vector3(j + .5f, i + .5f, 0);
				}
			}
		}
		return FoundWaypoints;
	}
	
	private Vector3 FindPointByIndex(int index, int[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] == index)
				{
					return new Vector3(i, j, 0);
				}
			}
		}
		return new Vector3(-1,-1,-1);
	}
	
	// Mirror the map verically so bool array matches
	// screen map.
	private int[,] FixMap(int[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				int temp = m[i,j];
				m[i,j] = m[m.GetLength(0) -i -1, j];
				m[i=m.GetLength(0) -i -1,j] = temp;
			}
			
			// if i is >= m.getLength(0) -i -1 then the map has been mirrored to
			// the half point and needs to be returned.
			// if the map continues to mirror it will mirrow back to its
			// origianl layout
			if( (i >= m.GetLength(0) -i -1) )
			{
				return m;
			}
		}
		return m; // should never reach this
	}
	
	private bool[,] IntMapToBool(int[,] m)
	{
		bool[,] b = new bool[m.GetLength(0), m.GetLength(1)];
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				b[i,j] = m[i,j] == (int) Global.MapToken.Tower;
			}
		}
		return b;
	}
	#endregion



	

	

	

}
