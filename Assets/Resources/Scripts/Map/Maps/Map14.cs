using UnityEngine;																														
using System.Collections;																														

public class Map14 : GameMap {																														
	public override Map GetMap ()																													
	{																													
		int[,] intmap = new int[,] 																												
		{																												
			{	0,	P,	P,	P,	P,	P,	P,	P,	1,	T,	T,	4,	P,	P,	5},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	P,	T,	T,	P,	P,	T,	T},												
			{	T,	T,	T,	T,	T,	T,	T,	P,	2,	P,	P,	3,	P,	T,	T}												
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