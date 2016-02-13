using UnityEngine;																														
using System.Collections;																														

public class Map00 : GameMap {																														
	public override Map GetMap ()																													
	{
         int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	A,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	A,	B,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	A,	A,	B,	B,	B,	C,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	B,	B,	B,	B,	C,	C,	C,	0,	0,	0,	0,	0,	0}
        };

        int[,] intmap = new int[,] {
            {P,2,P,3,T,6,P,7,T,10,P,11,P},
            {T,P,T,P,T,P,T,P,T,P, T,P, T},
            {T,P,T,P,T,P,T,P,T,P, T,P, T},
            {T,P,T,P,T,P,T,P,T,P, T,12,13},
            {T,P,T,P,T,P,T,P,T,P, T,P, P},
            {T,P,T,P,T,P,T,P,T,P, T,P, P},
            {0,1,T,4,P,5,T,8,P,9, T,P, P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}