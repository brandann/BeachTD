using UnityEngine;																														
using System.Collections;																														

public class Map05 : GameMap {																														
	public override Map GetMap ()																													
	{
        int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	A,	A,	A,	A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{C,	A,	A,	A,	A,	A,	A,	A,	A,	C,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0}
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