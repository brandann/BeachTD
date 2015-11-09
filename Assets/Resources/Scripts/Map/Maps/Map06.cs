using UnityEngine;																														
using System.Collections;																														

public class Map06 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,B,C,B,A,B},
            {B,C,B,A,B,C},
            {C,B,A,B,C,B},
            {B,A,B,C,B,A},
            {A,B,C,B,A,B},
            {B,C,B,A,B,C},
            {C,B,A,B,C,B}
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