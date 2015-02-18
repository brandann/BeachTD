using UnityEngine;
using System.Collections.Generic;

public class Wave {

	private List<EnemySchedule> wave;
	
	public Wave() 
	{
		wave = new List<EnemySchedule>();
	}
	
	public void AddScheduleItem(EnemySchedule item) 
	{
		wave.Add(item);
	}
	
	public List<EnemySchedule> GetWave() 
	{
		return wave;
	}
	
	public void SetScheduleItem(int index, EnemySchedule item) 
	{
		if(index < 0 || index > wave.Count)
		{
			return;
		}
		wave[index] = item;
	}
	
	public EnemySchedule GetScheduleItem(int index) 
	{
		if(index < 0 || index >= wave.Count)
		{
			return null;
		}
		return wave[index];
	}
	
	public EnemySchedule GetNextScheduleItem()
	{
		if(wave.Count > 0)
		{
			EnemySchedule t = wave[0];
			wave.RemoveAt(0);
			return t;
		}
		return null;
	}
	
	public int Count()
	{
		return wave.Count;
	}
}
