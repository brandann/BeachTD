﻿using UnityEngine;																														
using System.Collections;																														

public class Map05 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	P},
			{	0,	P,	P,	1,	T,	P,	T,	4,	P,	P,	P,	P,	5,	T,	P},
			{	T,	T,	T,	P,	T,	P,	T,	P,	T,	T,	T,	T,	P,	T,	T},
			{	P,	P,	T,	P,	T,	P,	T,	P,	T,	P,	P,	T,	6,	P,	7},
			{	P,	P,	T,	P,	T,	P,	T,	P,	T,	P,	P,	T,	T,	T,	T},
			{	P,	P,	T,	P,	T,	T,	T,	P,	T,	P,	P,	P,	P,	P,	P},
			{	P,	P,	T,	2,	P,	P,	P,	3,	T,	P,	P,	P,	P,	P,	P},
			{	P,	P,	T,	T,	T,	T,	T,	T,	T,	P,	P,	P,	P,	P,	P}												
		};																												
		
		Map map = new Map(intmap);																												
		
		Wave wave01 = new Wave();																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																												
		map.AddWave(wave01);																												
		
		return map;																												
	}																													
}																														