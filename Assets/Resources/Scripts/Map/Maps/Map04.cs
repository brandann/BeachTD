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
			{	P,	T,	T,	T,	P,	T,	P,	P,	P,	T,	P,	T,	T,	T,	P},
			{	P,	T,	T,	T,	P,	T,	P,	P,	P,	T,	4,	P,	P,	P,	5},
			{	0,	P,	P,	P,	1,	T,	P,	P,	P,	T,	T,	T,	T,	T,	P},
			{	P,	T,	T,	T,	T,	T,	P,	P,	P,	P,	P,	P,	P,	P,	P}											
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