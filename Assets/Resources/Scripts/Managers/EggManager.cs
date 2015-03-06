using UnityEngine;
using System.Collections.Generic;

public class EggManager : ManagerBase {

    public delegate void EggCountChanged(int count);
    public static event EggCountChanged OnEggCountChanged;

	#region Private Memebers
	private const int STARTING_EGG_COUNT = 3;
	private int _eggsOnPath;
	private int _eggsWithEnemy;
	public enum EggLocations{Path, Enemy, End, Start, Bird}
	#endregion
	
	#region Public Methods
	public EggManager()
	{
		_eggsOnPath = 0;
		_eggsWithEnemy = 0;
	}
	
	public void TransferEgg(EggLocations from, EggLocations to)
	{
		if(from == to)
		{
			Debug.LogError("TransferEgg: from cannot == to");
			return;
		}
		
		switch(from)
		{
			case(EggLocations.End):
				Remove();
				break;
			case(EggLocations.Enemy):
				_eggsWithEnemy--;
				break;
			case(EggLocations.Path):
				_eggsOnPath--;
				break;
			case(EggLocations.Start):
				break;
			case(EggLocations.Bird):
				Remove();
				break;
		}
		
		switch(to)
		{
			case(EggLocations.End):
				break;
			case(EggLocations.Enemy):
				_eggsWithEnemy++;
				break;
			case(EggLocations.Path):
				_eggsOnPath++;
				break;
			case(EggLocations.Start):
				break;
			case(EggLocations.Bird):
				break;
		}
		
		//Inform subscribers of change in number of eggs
		if (OnEggCountChanged != null)
			OnEggCountChanged(EggCount);
		
		if(EggCount == 0)
		{
			_global.WinLoseCond();
		}
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
			egg.GetComponent<Collider2D>().enabled = false;
		}

        if (OnEggCountChanged != null)
            OnEggCountChanged(STARTING_EGG_COUNT);
	}
	
	public void DropEgg(Vector3 location)
	{
		TransferEgg(EggLocations.Enemy, EggLocations.Path);
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		GameObject egg = GameObject.Instantiate(EggPrefab) as GameObject;
		egg.transform.position = location;
		egg.GetComponent<CircleCollider2D>().enabled = true;
	}
	
	public int EggCount
	{
		get { return GetActiveCount() + _eggsOnPath + _eggsWithEnemy; }
	}
	#endregion
	
	#region Private Methods

	#endregion
}
