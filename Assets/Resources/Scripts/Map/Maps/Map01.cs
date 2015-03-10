using UnityEngine;																				
using System.Collections;																				

public class Map01 : GameMap {																				
	public override Map GetMap ()																			
	{																			
		int[,] intmap = new int[,] 																		
		{																		
			{	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P},
			{	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P},
			{	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P},
			{	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	P},
			{	0,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	1},
			{	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P},
			{	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P},
			{	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P}	
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