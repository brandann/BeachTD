using UnityEngine;																														
using System.Collections;																														

public class Map21 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	1,	T,	4,	P,	5,	T,	8,	P,	9,	T,	12,	P,	13,	T},												
			{	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T},												
			{	T,	2,	P,	3,	T,	6,	P,	7,	T,	10,	P,	11,	T,	14,	15},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
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