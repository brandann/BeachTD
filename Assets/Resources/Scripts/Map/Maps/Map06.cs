using UnityEngine;																														
using System.Collections;																														

public class Map06 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},
            {	0,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	1,	T},
            {	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	T},
            {	T,	3,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	2,	T},
            {	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},
            {	T,	P,	T,	6,	P,	7,	T,	10,	P,	11,	T,	14,	P,	15,	T},
            {	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	16,	17},
            {	T,	4,	P,	5,	T,	8,	P,	9,	T,	12,	P,	13,	T,	T,	T}										
		};																												
		
		Map map = new Map(intmap);																												
		
		Wave wave01 = new Wave();
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .9f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .9f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .9f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		map.AddWave(wave01);																												
		
		Wave wave02 = new Wave();
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.125f));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.125f));																					
		map.AddWave(wave02);
		
		Wave wave03 = new Wave();
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																		
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1f));																					
		map.AddWave(wave03);
		
		Wave wave04 = new Wave();
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));																		
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));
		map.AddWave(wave04);																											
		
		return map;																												
	}																													
}																														