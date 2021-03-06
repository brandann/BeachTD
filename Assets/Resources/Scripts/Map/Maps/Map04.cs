﻿using UnityEngine;																														
using System.Collections;																														

public class Map04 : GameMap {																														
	public override Map GetMap ()																													
	{
		
        int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	B,	A,	B,	B,	A,	A,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{C,	A,	A,	A,	A,	B,	B,	B,	B,	B,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{C,	C,	B,	B,	A,	A,	A,	A,	A,	A,	A,	A,	B,	B,	C,	0,	0,	0,	0,	0},
			{C,	C,	C,	A,	A,	A,	A,	A,	A,	A,	B,	B,	B,	B,	B,	C,	C,	C,	0,	0},
			{B,	B,	C,	B,	B,	C,	B,	B,	C,	B,	B,	C,	B,	B,	C,	B,	B,	C,	0,	0}
        };

        int[,] intmap = new int[,] {
            {P,T,3,P,P,P,P,P,P,2,T,T,P},
            {T,T,P,T,T,T,T,T,T,P,T,T,T},
            {T,T,P,T,T,T,6,P,P,P,P,P,7},
            {T,T,P,T,T,T,P,T,T,P,T,T,T},
            {0,P,P,P,P,P,P,P,P,1,T,T,T},
            {T,T,P,T,T,T,P,T,T,T,T,T,T},
            {P,T,4,P,P,P,5,T,T,T,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}