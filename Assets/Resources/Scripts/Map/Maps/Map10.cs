using UnityEngine;																														
using System.Collections;																														

public class Map10 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	6,	P,	P,	7,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	P,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	P,	T,	T,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P},												
			{	T,	T,	5,	P,	4,	P,	T,	T,	10,	P,	P,	P,	P,	P,	11},												
			{	0,	1,	T,	T,	P,	P,	T,	T,	P,	T,	T,	T,	T,	T,	T},												
			{	T,	P,	T,	T,	P,	8,	P,	P,	9,	T,	T,	T,	T,	T,	T},												
			{	T,	2,	P,	P,	3,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
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