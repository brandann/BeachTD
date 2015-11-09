﻿using UnityEngine;																														
using System.Collections;																														

public class Map08 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,A,B,B,B,C,C},
            {A,A,A,B,B,B,C,C},
            {A,A,A,B,B,B,C,C},
            {B,B,B,B,B,B,C,C},
            {B,B,B,B,B,B,C,C},
            {B,B,B,B,B,B,C,C},
            {A,A,A,B,B,B,C,C},
            {A,A,A,B,B,B,C,C},
            {A,A,A,B,B,B,C,C}
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