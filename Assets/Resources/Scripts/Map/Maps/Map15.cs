using UnityEngine;																														
using System.Collections;																														

public class Map15 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T},												
			{	0,	P,	P,	P,	P,	1,	T,	T,	T,	4,	P,	P,	P,	P,	1},												
			{	P,	T,	P,	T,	P,	P,	P,	T,	P,	P,	P,	T,	P,	T,	P},												
			{	T,	T,	T,	T,	T,	P,	T,	T,	T,	P,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	P,	T,	P,	T,	P,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	2,	P,	P,	P,	3,	T,	T,	T,	T,	T}												
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