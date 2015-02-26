using UnityEngine;																														
using System.Collections;																														

public class Map23 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	16,	P,	P,	P,	P,	P,	17},												
			{	T,	T,	T,	T,	T,	T,	T,	14,	15,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	12,	13,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	1,	T,	T,	T,	10,	11,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	T,	T,	8,	9,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	T,	6,	7,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	4,	5,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	2,	3,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T}												
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