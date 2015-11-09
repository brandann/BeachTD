using UnityEngine;																														
using System.Collections;																														

public class Map05 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,B,B,C,C},
            {A,B,B,C,C,B},
            {B,B,C,C,B,B},
            {B,C,C,B,B,A},
            {C,C,B,B,A,A},
            {C,B,B,A,A,B}
        };

        int[,] intmap = new int[,] {
            {P,4,P,P,P,P,P,P,P,P,P,5,P},
            {T,P,T,T,T,T,T,T,T,T,T,P,T},
            {T,P,T,T,T,T,T,T,T,T,T,P,T},
            {T,3,P,P,2,T,T,T,7,P,P,6,T},
            {T,T,T,T,P,T,T,T,P,T,T,T,T},
            {T,T,T,T,P,T,T,T,P,T,T,T,T},
            {0,P,P,P,1,T,T,T,8,P,P,P,9}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}