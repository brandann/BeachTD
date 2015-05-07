using UnityEngine;																														
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