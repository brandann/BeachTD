using UnityEngine;																				
using System.Collections;																				

public class Map01 : GameMap {																				
	public override Map GetMap ()																			
	{
        int[,] waves = new int[,] {
            {A,A,A,A,A},
            {A,A,A,B,B}
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