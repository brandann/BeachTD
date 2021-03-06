﻿using UnityEngine;																														
using System.Collections;																														

public class Map06 : GameMap {																														
	public override Map GetMap ()																													
	{
		        int[,] waves = new int[,] {
{A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
{A,	A,	A,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
{A,	A,	A,	A,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
{A,	A,	B,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
{A,	A,	A,	A,	A,	B,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
{A,	A,	A,	A,	A,	A,	A,	A,	A,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0}
        };

        int[,] intmap = new int[,] {
            {T,2,P,3,T,T,T,T,T,T,T,T,P},
            {T,P,T,P,T,6,P,7,T,T,T,T,T},
            {T,P,T,P,T,P,T,P,T,10,P,11,T},
            {0,1,T,P,T,P,T,P,T,P,T,12,13},
            {T,T,T,P,T,P,T,8,P,9,T,T,T},
            {T,T,T,4,P,5,T,T,T,T,T,T,T},
            {P,T,T,T,T,T,T,T,T,T,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}