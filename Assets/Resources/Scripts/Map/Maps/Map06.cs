using UnityEngine;																														
using System.Collections;																														

public class Map06 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	1,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	T},												
			{	T,	3,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	2,	T},												
			{	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	T,	6,	T,	T,	T,	10,	P,	11,	T,	14,	P,	15,	T},												
			{	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	16,	17},												
			{	T,	4,	P,	5,	T,	8,	P,	9,	T,	12,	P,	13,	T,	T,	T},												
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