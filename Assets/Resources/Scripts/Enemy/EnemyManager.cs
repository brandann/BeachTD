using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    Global global;
    private Dictionary<int, GameObject> _enemies;
    private GameObject EnemyA0Prefab;
    private GameObject EnemyB0Prefab;
    private GameObject EnemyC0Prefab;

    private float _spawntimedinterval = 0;
    private float randomSpawnTime = 0;

    private bool _mapLoaded = false;

	// Use this for initialization
	void Start ()
    {
        global = GetComponent<Global>();
        _enemies = new Dictionary<int, GameObject>();
        EnemyA0Prefab = Resources.Load("Prefabs/EnemyA0") as GameObject;
        EnemyB0Prefab = Resources.Load("Prefabs/EnemyB0") as GameObject;
        EnemyC0Prefab = Resources.Load("Prefabs/EnemyC0") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        RandomEnemySpawner(_mapLoaded);

        if (Input.GetKeyUp(KeyCode.Alpha1)) { SpawnEnemy(EnemyA0Prefab); }
        else if (Input.GetKeyUp(KeyCode.Alpha2)) { SpawnEnemy(EnemyB0Prefab); }
        else if (Input.GetKeyUp(KeyCode.Alpha3)) { SpawnEnemy(EnemyC0Prefab); }
	}

    public void StartRandomEnemySpawner()
    {
        _mapLoaded = true;
    }

    public void SpawnEnemy(GameObject EnemyPrefab)
    {
        GameObject go = global.SpawnPrefab(EnemyPrefab, global.CurrentMap.Waypoints[0]);
        _enemies.Add(go.GetInstanceID(), go);
    }

    public void Reset()
    {
        if (_enemies != null)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Destroy(_enemies[i]);
            }
        }
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
                SpawnEnemy(enemy);
            }
            _spawntimedinterval = Time.realtimeSinceStartup;
            //randomSpawnTime = Random.Range(RandomEnemySpawnLow, RandomEnemySpawnHigh);
            randomSpawnTime = 3;
        }
    }
}
