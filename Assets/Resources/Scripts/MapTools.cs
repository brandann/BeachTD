using UnityEngine;
using System.Collections;

public class MapTools {

	// Mirror the map verically so bool array matches
	// screen map.
	public bool[,] FixMap(bool[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				bool temp = m[i,j];
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
	
	public Vector3[] FixWaypoints(Vector3[] v, float s = 1)
	{
		for(int i = 0; i < v.GetLength(0); i++)
		{
			v[i].x = (v[i].x * s) + (s/2);
			v[i].y = (v[i].y * s) + (s/2);
		}
		return v;
	}
	
}
