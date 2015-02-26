using UnityEngine;																														
using System.Collections;																														

public class Map13 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	6,	P,	7,	T,	P,	T,	10,	P,	11,	T,	T,	T},												
			{	T,	T,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	T,	T},												
			{	T,	T,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	T,	T},												
			{	T,	T,	T,	P,	T,	P,	T,	T,	T,	P,	T,	P,	T,	T,	T},												
			{	T,	T,	4,	5,	T,	8,	P,	P,	P,	9,	T,	12,	P,	P,	13},												
			{	T,	2,	3,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	1,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T}												
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