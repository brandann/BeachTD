﻿using UnityEngine;																														
using System.Collections;																														

public class Map08 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},
            {	T,	2,	P,	3,	T,	T,	T,	T,	T,	T,	T,	6,	P,	7,	T},
            {	T,	P,	T,	P,	T,	T,	T,	T,	T,	T,	T,	P,	T,	P,	T},
            {	T,	P,	T,	P,	T,	T,	T,	T,	T,	T,	T,	P,	T,	P,	T},
            {	T,	P,	T,	P,	T,	T,	T,	T,	T,	T,	T,	P,	T,	P,	T},
            {	T,	P,	T,	P,	T,	T,	T,	T,	T,	T,	T,	P,	T,	P,	T},
            {	0,	1,	T,	4,	P,	P,	P,	P,	P,	P,	P,	5,	T,	8,	9},
            {	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T}											
		};																												
		
		Map map = new Map(intmap);																												
		
		Wave wave01 = new Wave();	
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1.7f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, 1f));																												
		map.AddWave(wave01);																												
		
		Wave wave02 = new Wave();
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,    1));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, .7f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,    1));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   .9f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   .9f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,   .9f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   .9f));
		map.AddWave(wave02);
		
		Wave wave03 = new Wave();
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));																		
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0,   1.1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1.1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1.1f));
		wave03.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,   1.1f));
		map.AddWave(wave03);
		
		Wave wave04 = new Wave();
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .9f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .9f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .9f));																		
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,  1.25f));
		wave04.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0,  1.25f));
		map.AddWave(wave04);			
		
		Wave wave05 = new Wave();
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .7f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));																												
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));
		wave05.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, .7f));
		map.AddWave(wave05);																									
		
		return map;																												
	}																													
}																														