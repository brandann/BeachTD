using UnityEngine;
using System.Collections.Generic;

public class EggManager : ManagerBase {

    public delegate void EggCountChanged(int count);
    public static event EggCountChanged OnEggCountChanged;

    public int EggCount
    {
        get
        {
            return _eggsOnPath.Count + _eggsWithEnemy.Count;
        }
    }


	#region Private Memebers
	private int STARTING_EGG_COUNT;
	private List<Egg> _eggsOnPath;
	private List<Egg> _eggsWithEnemy;
	
	#endregion
	
	#region Public Methods
	public EggManager()
	{
        _eggsOnPath = new List<Egg>();
        _eggsWithEnemy = new List<Egg>();
        Egg.EggPickedUp += HandleEggPickup;
        Egg.EggDropped += HandleEggDrop;
        Egg.EggKilled += HandleEggKilled;

	}

    ~EggManager()
    {
        Egg.EggPickedUp -= HandleEggPickup;
        Egg.EggDropped -= HandleEggDrop;
        Egg.EggKilled -= HandleEggKilled;
    }


    public void SpawnEgg(int starteggcount)
    {
    	STARTING_EGG_COUNT = starteggcount;
        GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
        for (int i = 0; i < STARTING_EGG_COUNT; i++)
        {
            Vector3 endloc = _global.CurrentMap.Waypoints[_global.CurrentMap.Waypoints.Length - 1];
            Vector3 loc;

            // -- RANDOM
            Vector3 randloc = new Vector3(Random.Range(-1f, 0f), Random.Range(-0.30f, 0.30f), 0);
            randloc.x -= 0;
            loc = randloc;
            // -- END RANDOM

            Vector3 offset = new Vector3(0, 0, 0);

            GameObject egg = Create(EggPrefab, endloc + loc + offset);

            _eggsOnPath.Add(egg.GetComponent<Egg>());
        }

        if (OnEggCountChanged != null)
            OnEggCountChanged(EggCount);
    }

    public void BroadcastEggCount()
    {
        if (OnEggCountChanged != null)
            OnEggCountChanged(EggCount);
    }

	
	#endregion
	
	#region Private Methods

    private void HandleEggPickup(Egg egg)
    {       
        _eggsOnPath.Remove(egg);
        _eggsWithEnemy.Add(egg);
    }

    private void HandleEggDrop(Egg egg)
    {
        _eggsWithEnemy.Remove(egg);
        _eggsOnPath.Add(egg);
    }

    private void HandleEggKilled(Egg egg)
    {
        //Egg should never be in both lists but just to be safe
        _eggsOnPath.Remove(egg);
        _eggsWithEnemy.Remove(egg);

        GameObject.Destroy(egg.gameObject);

        if (OnEggCountChanged != null)
            OnEggCountChanged(EggCount);

        _global.WinLoseCond();

        
    }

	#endregion
}
