﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMap {

	protected int P = (int) Global.MapToken.Path;
	protected int T = (int) Global.MapToken.Tower;
	
	public virtual Map GetMap()
	{
		return null;
	}
	
	// Mirror the map verically so bool array matches
	// screen map.
	protected int[,] FixMap(int[,] m)
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
			if( (i >= m.GetLength(0) -i) )
			{
				return m;
			}
		}
		Debug.LogError("Map Fix is Bad");
		return m; // should never reach this
	}
}
