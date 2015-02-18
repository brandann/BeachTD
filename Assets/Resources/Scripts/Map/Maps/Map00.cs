using UnityEngine;																				
using System.Collections;																				

public class Map00 : GameMap {																				
	public override Map GetMap ()																			
	{																			
		int[,] intmap = new int[,] 																		
		{																		
			{	T,	T,	T,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	P},		
			{	P,	P,	P,	P,	T,	P,	T,	P,	P,	P,	P,	P,	0,	T,	P},		
			{	T,	T,	T,	P,	T,	P,	T,	P,	T,	T,	T,	T,	P,	T,	T},		
			{	P,	P,	T,	P,	T,	P,	T,	P,	T,	P,	P,	T,	1,	P,	2},		
			{	P,	P,	T,	P,	T,	P,	T,	P,	T,	P,	P,	T,	T,	T,	T},		
			{	P,	P,	T,	P,	T,	T,	T,	P,	T,	P,	P,	P,	P,	P,	P},		
			{	P,	P,	T,	P,	P,	P,	P,	P,	T,	P,	P,	P,	P,	P,	P},		
			{	P,	P,	T,	T,	T,	T,	T,	T,	T,	P,	P,	P,	P,	P,	P},		
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