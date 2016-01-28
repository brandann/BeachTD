using UnityEngine;																														
using System.Collections;																														

public class Map06 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,A,A,0,0,0,0,0,0},
            {A,A,A,B,B,0,0,0,0,0},
            {A,A,A,A,C,0,0,0,0,0},
            {A,A,B,B,B,0,0,0,0,0},
            {A,A,A,A,A,B,C,0,0,0},
            {A,A,A,A,A,A,A,A,A,C}
        };

        int[,] intmap = new int[,] {
            {P,2,3,T,T,T,T,T,T,T,T,T,P},
            {T,P,4,5,T,T,T,T,T,T,T,T,T},
            {T,P,T,6,7,T,T,T,T,16,P,P,17},
            {T,P,T,T,8,9,T,T,T,P,T,T,T},
            {0,1,T,T,T,10,11,T,T,P,T,T,T},
            {T,T,T,T,T,T,12,13,T,P,T,T,T},
            {P,T,T,T,T,T,T,14,P,15,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}