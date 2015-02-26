using UnityEngine;																														
using System.Collections;																														

public class Map18 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	T,	T,	T,	T,	T,	T,	6,	P,	P,	P,	P,	P,	P,	P,	7},												
			{	T,	T,	T,	T,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	4,	P,	P,	P,	5,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	3,	P,	P,	2,	2,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	0,	P,	P,	P,	P,	P,	1,	T,	T,	T,	T,	T,	T,	T,	T}												
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