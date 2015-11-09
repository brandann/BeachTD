using UnityEngine;																														
using System.Collections;																														

public class Map09 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,B,B,C,C,B,B,A},
            {A,A,B,B,C,C,B,B,A},
            {B,B,A,A,B,B,C,C,B},
            {B,B,A,A,B,B,C,C,B},
            {C,C,B,B,A,A,B,B,C},
            {C,C,B,B,A,A,B,B,C},
            {B,B,C,C,B,B,A,A,B},
            {B,B,C,C,B,B,A,A,B},
            {A,A,B,B,C,C,B,B,A},
            {A,A,B,B,C,C,B,B,A}
        };

        int[,] intmap = new int[,] {
            {P,2,P,3,T,6,P,7,T,10,P,11,P},
            {T,P,T,P,T,P,T,P,T,P,T,P,T},
            {T,P,T,P,T,P,T,P,T,P,T,P,T},
            {T,P,T,P,T,P,T,P,T,P,T,P,14},
            {T,P,T,P,T,P,T,P,T,P,T,P,P},
            {T,P,T,P,T,P,T,P,T,P,T,P,P},
            {0,1,T,4,P,5,T,8,P,9,T,12,13}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}