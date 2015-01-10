using UnityEngine;
using System.Collections;

public class Map000 : LevelMap {

	public Map000()
	{
		this._hitpointsmax = this.HitPoints = 10;
		this.Map = new MapTools().FixMap(new bool[,]
		{	
			{	O,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	O,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	O,	I,	I,	I,	I,	O,	O,	O,	O,	O,	O,	O	},
			{	O,	I,	I,	I,	I,	O,	I,	I,	I,	I,	I,	I	},
			{	O,	O,	O,	O,	O,	O,	I,	I,	I,	O,	O,	O	},
			{	O,	I,	I,	I,	I,	I,	I,	I,	I,	O,	O,	O	},
			{	O,	I,	I,	I,	I,	I,	I,	I,	I,	O,	O,	O	},
		});
		this.Waypoints = new MapTools().FixWaypoints(new Vector3[]
		{
			new Vector3(0,2,0),
			new Vector3(5,2,0),
			new Vector3(5,4,0),
			new Vector3(11,4,0)
		});
	}
}
