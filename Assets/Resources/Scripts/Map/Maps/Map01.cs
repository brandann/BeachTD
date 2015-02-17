using UnityEngine;
using System.Collections;

public class Map01 : GameMap {

	//----------------------------------------
	//                INSTRUCTIONS
	//
	// 1. Make new int[,] map
	// 2. make new map with int[,]
	// 3. make all waves
	// 4. return map
	//
	//----------------------------------------
	
	public override Map GetMap ()
	{
		int[,] intmap = new int[,]
		{
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T},//1
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//2
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//3
			{P,P,P,P,P,P,P,P,P,T,T,T,T,T,P},//4
			{P,P,P,P,P,P,P,P,0,P,P,P,P,P,1},//5
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//6
			{P,P,P,P,P,P,P,P,P,P,P,P,P,P,P},//7
			{T,P,P,P,P,P,P,P,P,P,P,P,P,P,T} //8
		};
		
		int[,] tempmap = FixMap(intmap);
		
		Map map = new Map(tempmap);
		
		Wave wave01 = new Wave();
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave01.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		map.AddWave(wave01);
		
		Wave wave02 = new Wave();
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		wave02.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, .75f));
		map.AddWave(wave02);
		
		return map;
	}
}
