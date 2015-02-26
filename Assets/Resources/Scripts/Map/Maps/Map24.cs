using UnityEngine;																														
using System.Collections;																														

public class Map24 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	6,	7},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	4,	5,	T},												
			{	T,	T,	P,	P,	P,	P,	P,	P,	P,	T,	T,	2,	3,	T,	T},												
			{	0,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	1,	T,	T,	T},												
			{	T,	T,	P,	P,	P,	P,	P,	P,	P,	T,	T,	T,	T,	T,	T},												
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