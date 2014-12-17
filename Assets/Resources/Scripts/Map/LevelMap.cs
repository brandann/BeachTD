using UnityEngine;
using System.Collections;

public class LevelMap {

	#region private members
	private bool[,] 	_map;		// bool array of tower accessable objects
	private Vector3[] 	_waypoints;	// enemys path for map
	private int			_hitpoints;	// "lives" for level
	private float		_scale;		// level scale
	protected int 		_hitpointsmax;
	protected const bool I = true;	// for ease of reading
	protected const bool O = false;	// for ease of reading
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
	#endregion
	
	#region Private Methods

	#endregion
	
}
