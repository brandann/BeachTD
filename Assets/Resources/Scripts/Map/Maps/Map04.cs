using UnityEngine;																														
using System.Collections;																														

public class Map04 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	P,	P,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	P,	P},
			{	P,	P,	P,	T,	2,	P,	P,	P,	P,	P,	3,	T,	P,	P,	P},
			{	P,	P,	P,	T,	P,	T,	T,	T,	T,	T,	P,	T,	P,	P,	P},
			{	P,	P,	P,	T,	P,	T,	P,	P,	P,	T,	P,	T,	P,	P,	P},
			{	P,	P,  P,	T,	P,	T,	P,	P,	P,	T,	P,	T,	T,	T,	P},
			{	P,	T,	T,	T,	P,	T,	P,	P,	P,	T,	4,	P,	P,	P,	5},
			{	0,	P,	P,	P,	1,	T,	P,	P,	P,	T,	T,	T,	T,	T,	P},
			{	P,	T,	T,	T,	T,	T,	P,	P,	P,	P,	P,	P,	P,	P,	P}											
		};																												
		
		Map map = new Map(intmap);																												
		
		Wave wave01 = new Wave();
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		map.AddWave(wave01);																												
		
		Wave wave02 = new Wave();
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,    1));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,    1));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   5f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   5f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   5f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   5f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   5f));
		map.AddWave(wave02);
		
		Wave wave03 = new Wave();
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																		
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   3f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   3f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   5f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   5f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   5f));
		map.AddWave(wave03);																												
		
		return map;																												
	}																													
}																														