using UnityEngine;																				
using System.Collections;																				

public class Map00 : GameMap {																				
	public override Map GetMap ()																			
	{																			
		int[,] intmap = new int[,] 																		
		{																		
			{	0,	1,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},	
			{	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},	
			{	T,	2,	P,	3,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},	
			{	T,	T,	T,	P,	T,	6,	P,	7,	T,	10,	P,	11,	T,	14,	15},	
			{	T,	T,	T,	P,	T,	P,	T,	P,	T,	P,	T,	12,	P,	13,	T},	
			{	T,	T,	T,	P,	T,	P,	T,	8,	P,	9,	T,	T,	T,	T,	T},	
			{	T,	T,	T,	P,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T},	
			{	T,	T,	T,	4,	P,	5,	T,	T,	T,	T,	T,	T,	T,	T,	T},			
		};																		
		
		Map map = new Map(intmap);																		
		
		Wave wave01 = new Wave();	
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																	
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																		
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																																			
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, 3f));																		
		map.AddWave(wave01);	
		
		Wave wave02 = new Wave();
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));																			
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																		
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));																																				
		map.AddWave(wave02);																		
		
		return map;																		
	}																			
}																				