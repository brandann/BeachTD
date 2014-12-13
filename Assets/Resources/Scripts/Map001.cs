using UnityEngine;
using System.Collections;

public class Map001 : LevelMap {

	public Map001(float s)
	{
		this.Scale = s;
		this._hitpointsmax = this.HitPoints = 10;
		this.Map = new bool[,]
		{	//  0   1   2   3   4   5   6   7   8   9  10  11
			{	O,	O,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},//6
			{	I,	O,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},//5
			{	I,	O,	I,	I,	I,	O,	O,	O,	I,	O,	O,	O	},//4
			{	I,	O,	I,	I,	I,	O,	I,	O,	I,	O,	I,	I	},//3
			{	I,	O,	O,	O,	O,	O,	I,	O,	O,	O,	I,	I	},//2
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},//1
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},//0
		};
		this.Waypoints = new Vector3[]
		{
			new Vector3(0,6,0),
			new Vector3(1,6,0),
			new Vector3(1,2,0),
			new Vector3(5,2,0),
			new Vector3(5,4,0),
			new Vector3(7,4,0),
			new Vector3(7,2,0),
			new Vector3(9,2,0),
			new Vector3(9,4,0),
			new Vector3(11,4,0)
		};
	}
}
