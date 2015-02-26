using UnityEngine;																														
using System.Collections;																														

public class Map11 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	0,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	1,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	P,	T,	T},												
			{	T,	T,	3,	P,	P,	P,	P,	P,	P,	P,	P,	P,	2,	T,	T},												
			{	T,	T,	4,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P,	T,	P},												
			{	T,	T,	P,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T,	T},												
			{	T,	T,	4,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	P,	5}												
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