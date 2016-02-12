using UnityEngine;																														
using System.Collections;																														

public class Map02 : GameMap {																														
	public override Map GetMap ()																													
	{
		int[,] waves = new int[,] {
			{A,	A,	A,	A,	A,	A,	A,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	A,	A,	A,	A,	A,	A,	A,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{B,	B,	A,	A,	B,	B,	A,	A,	B,	B,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0},
			{A,	B,	B,	B,	A,	B,	B,	B,	A,	B,	B,	B,	A,	B,	B,	0,	0,	0,	0,	0},
			{C,	C,	A,	A,	A,	B,	B,	B,	B,	B,	B,	B,	B,	A,	A,	A,	C,	C,	0,	0}
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