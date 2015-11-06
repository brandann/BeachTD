using UnityEngine;																														
using System.Collections;																														

public class Map09 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	P,	P,	P,	P,	1,	T,	T,	T,	8,	P,	P,	P,	P,	9},												
			{	T,	T,	T,	T,	T,	P,	T,	T,	T,	P,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	P,	T,	T,	T,	P,	T,	T,	T,	T,	T},												
			{	T,	T,	3,	P,	P,	2,	T,	T,	T,	7,	P,	P,	6,	T,	T},												
			{	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	T,	T},												
			{	T,	T,	4,	P,	P,	P,	P,	P,	P,	P,	P,	P,	5,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T}												
		};																												
		
		Map map = new Map(intmap);																												
		
		Wave wave01 = new Wave();	
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, 1f));																												
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
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1f));
		map.AddWave(wave02);
		
		Wave wave03 = new Wave();
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																		
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   .9f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		map.AddWave(wave03);
		
		Wave wave04 = new Wave();
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .25f));																		
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));
        wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));
		map.AddWave(wave04);
		
		Wave wave05 = new Wave();
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		map.AddWave(wave05);	
		
		Wave wave06 = new Wave();
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
        wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.25f));
        wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));
        wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1.25f));																												
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .75f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   .9f));
		wave06.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		map.AddWave(wave06);	
		return map;																												
	}																													
}																														