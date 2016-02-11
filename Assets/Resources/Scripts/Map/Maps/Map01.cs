using UnityEngine;																														
using System.Collections;																														

public class Map01 : GameMap {																														
	public override Map GetMap ()																													
	{
       
        int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	B,	B,	B,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	B,	B,	B,	B,	B,	B,	C,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	A,	A,	A,	A,	A,	A,	A,	C,	C,	C,	A,	A,	A,	A,	A,	C,	C,	0,	0},
			{A,	A,	A,	A,	B,	B,	B,	B,	B,	B,	C,	C,	C,	C,	C,	C,	C,	C,	C,	0},
			{C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C,	C}
        };

        int[,] intmap = new int[,] {
            {4,P,5,T,8,P,9,T,12,P,13,T,P},
            {P,T,P,T,P,T,P,T,P,T,P,T,T},
            {P,T,6,P,7,T,10,P,11,T,14,P,15},
            {P,T,T,T,T,T,T,T,T,T,T,T,T},
            {3,P,P,P,P,P,P,P,P,P,P,2,T},
            {T,T,T,T,T,T,T,T,T,T,T,P,T},
            {0,P,P,P,P,P,P,P,P,P,P,1,P}
        };

        Map map = new Map(intmap);
        map.setWaves(waves);

        return map;
    }
}