﻿using UnityEngine;																				
using System.Collections;																				

public class Map03 : GameMap {																				
	public override Map GetMap ()																			
	{
		 int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	A,	A,	A,	A,	A,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	B,	B,	B,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	B,	B,	B,	B,	C,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	B,	B,	A,	A,	A,	A,	A,	A,	A,	A,	A,	C,	C,	C,	C,	0,	0,	0,	0},
			{C,	C,	C,	C,	C,	A,	A,	A,	A,	A,	A,	A,	A,	A,	A,	A,	B,	C,	C,	C},
			{C,	B,	C,	B,	C,	B,	C,	B,	C,	B,	C,	B,	C,	B,	C,	B,	C,	B,	C,	B}
        };

        int[,] intmap = new int[,] {
            {P,T,T,T,T,T,T,T,T,T,T,T,P},
            {0,P,P,P,P,P,P,P,P,P,P,1,T},
            {T,T,T,T,T,T,T,T,T,T,T,P,T},
            {4,P,P,P,P,P,P,P,P,5,T,P,T},
            {P,T,T,T,T,T,T,T,T,T,T,P,T},
            {3,P,P,P,P,P,P,P,P,P,P,2,T},
            {P,T,T,T,T,T,T,T,T,T,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}