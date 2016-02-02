using UnityEngine;																														
using System.Collections;																														

public class Map07 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
            {A,A,A,A,A,0,0,0,0,0,0},
            {A,A,A,A,B,B,0,0,0,0,0},
            {A,A,A,A,A,A,A,A,A,A,0},
            {B,A,A,A,A,A,B,0,0,0,0},
            {A,A,A,A,A,A,A,A,A,A,C},
            {A,A,A,A,A,A,A,A,A,A,C}
        };
		
        int[,] intmap = new int[,] {
            {P,T,T,T,T,T,T,T,T,T,T,T,P},
            {T,2,P,P,3,T,T,T,T,T,T,T,T},
            {T,P,T,T,P,8,P,P,9,T,T,T,T},
            {0,1,T,T,P,P,T,T,P,T,T,T,T},
            {T,T,5,P,4,P,T,T,10,P,P,P,11},
            {T,T,P,T,T,P,T,T,T,T,T,T,T},
            {P,T,6,P,P,7,T,T,T,T,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}