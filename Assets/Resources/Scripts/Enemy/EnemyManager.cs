using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase {

    
    
    private GameObject EnemyA0Prefab;
    private GameObject EnemyB0Prefab;
    private GameObject EnemyC0Prefab;

    private float _spawntimedinterval = 0;
    private float randomSpawnTime = 0;

    private bool _mapLoaded = false;

	// Use this for initialization
    public EnemyManager() : base()
    {
		EnemyA0Prefab = Resources.Load("Prefabs/EnemyA0") as GameObject;
		EnemyB0Prefab = Resources.Load("Prefabs/EnemyB0") as GameObject;
		EnemyC0Prefab = Resources.Load("Prefabs/EnemyC0") as GameObject;
    }
	
	// Update is called once per frame
	public void Update () {
        RandomEnemySpawner(_mapLoaded);

        if (Input.GetKeyUp(KeyCode.Alpha1)) { Create(EnemyA0Prefab, _startingPosition); }
     	else if (Input.GetKeyUp(KeyCode.Alpha2)) { Create(EnemyB0Prefab, _startingPosition); }
  		else if (Input.GetKeyUp(KeyCode.Alpha3)) { Create(EnemyC0Prefab, _startingPosition); }
	}

    public void StartRandomEnemySpawner()
    {
        _mapLoaded = true;
    }

    private void RandomEnemySpawner(bool go)
    {
        if (!go) return;

        if ((Time.realtimeSinceStartup - _spawntimedinterval) > randomSpawnTime)
        {
            int r = Random.Range(0, 3);
            GameObject enemy = Resources.Load("Prefabs/EnemyA0") as GameObject;
            if (r == 1)
                enemy = Resources.Load("Prefabs/EnemyB0") as GameObject;
            else if (r == 2)
                enemy = Resources.Load("Prefabs/EnemyC0") as GameObject;
            if (enemy != null)
            {
                Create(enemy, _startingPosition);
            }
            _spawntimedinterval = Time.realtimeSinceStartup;
            //randomSpawnTime = Random.Range(RandomEnemySpawnLow, RandomEnemySpawnHigh);
            randomSpawnTime = 3;
        }
    }
}
