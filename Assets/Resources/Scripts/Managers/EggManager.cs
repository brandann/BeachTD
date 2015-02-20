using UnityEngine;
using System.Collections.Generic;

public class EggManager : ManagerBase {

    public delegate void EggCountChanged(int count);
    public static event EggCountChanged OnEggCountChanged;


	#region Private Memebers
	private const int STARTING_EGG_COUNT = 3;
	private int _eggsOnPath;
	private int _eggsWithEnemy;
	private enum EggCounter{Path, Enemy, Goal}
	#endregion
	
	#region Public Methods
	public EggManager()
	{
		_eggsOnPath = 0;
		_eggsWithEnemy = 0;
	}
	
	public void DropEgg(Vector3 pos)
	{
		Debug.Log("Dropegg");
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		GameObject egg = Create(EggPrefab, pos);
		egg.GetComponent<CircleCollider2D>().enabled = true;
	}
	
	public void PickUpEggFromPath()
	{
		DecrementEggCount(EggCounter.Path);
		_eggsWithEnemy++;
	}
	
	public void KillGoalEgg()
	{
		//Remove();
	}
	
	public void EnemyKillEgg()
	{
		DecrementEggCount(EggCounter.Enemy);
	}
	
	public void ArialKillEgg()
	{
		base.Remove();
	}
	
	public void SpawnEgg()
	{
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		for(int i = 0; i < STARTING_EGG_COUNT; i++)
		{
			Vector3 endloc = _global.CurrentMap.Waypoints[_global.CurrentMap.Waypoints.Length - 1];
			Vector3 randloc = new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-0.5f, 0.5f), 0);
			Vector3 offset = new Vector3(0,0,0);
			GameObject egg = Create(EggPrefab, endloc + randloc + offset);
			egg.collider2D.enabled = false;
		}

        if (OnEggCountChanged != null)
            OnEggCountChanged(STARTING_EGG_COUNT);
	}
	
	public override void Remove (GameObject go)
	{
		base.Remove (go);
		_eggsWithEnemy++;
		DecrementEggCount(EggCounter.Goal);
	}
	#endregion
	
	#region Private Methods
	private void DecrementEggCount(EggCounter ec)
	{
		switch(ec)
		{
			case(EggCounter.Path):
				_eggsOnPath--;
				break;
			case(EggCounter.Enemy):
				_eggsWithEnemy--;
				break;
			default:
				break;
		}
		int activeEggs = GetActiveCount() + _eggsOnPath + _eggsWithEnemy;

        //Inform subscribers of change in number of eggs
        if (OnEggCountChanged != null)
            OnEggCountChanged(activeEggs);

		if(activeEggs == 0)
		{
			_global.LoseCond();
		}

       
	}
	#endregion
}
