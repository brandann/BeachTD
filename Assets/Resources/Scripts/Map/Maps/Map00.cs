using UnityEngine;																				
using System.Collections;																				

public class Map00 : GameMap {																				
	public override Map GetMap ()																			
	{
        int[,] waves = new int[,] {
            {A,A,A,A,A,0},
            {A,A,A,A,A,B}
        };

        int[,] intmap = new int[,] {
            {P,T,T,T,T,T,T,T,T,T,T,T,P},
            {T,T,T,T,T,4,P,P,5,T,T,T,T},
            {T,T,T,T,T,P,T,T,P,T,T,T,T},
            {0,P,1,T,T,P,T,T,6,P,P,P,7},
            {T,T,P,T,T,P,T,T,T,T,T,T,T},
            {T,T,2,P,P,3,T,T,T,T,T,T,T},
            {P,T,T,T,T,T,T,T,T,T,T,T,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}