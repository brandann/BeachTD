using UnityEngine;																														
using System.Collections;																														

public class Map07 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,A,B,B,B,C},
            {A,A,B,B,B,C,C,},
            {A,B,B,B,C,C,C},
            {B,B,B,C,C,C,A},
            {B,B,C,C,C,A,B},
            {B,C,C,C,A,B,C},
            {C,C,C,A,B,C,B},
            {C,C,A,B,C,B,A}
        };

        int[,] intmap = new int[,] {
            {P,2,P,P,P,P,P,P,P,P,P,3,P},
            {T,P,T,T,T,T,T,T,T,T,T,P,T},
            {T,P,T,6,P,P,P,P,P,7,T,P,T},
            {0,1,T,P,T,T,T,T,T,P,T,P,T},
            {T,T,T,P,T,9,P,P,P,8,T,P,T},
            {T,T,T,P,T,T,T,T,T,T,T,P,T},
            {P,T,T,5,P,P,P,P,P,P,P,4,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}