﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {

	private LevelMap _currentMap;
	private Mapmanager _mapManager;
	private float _spawntimedinterval = 0;
	private float randomSpawnTime = 0;
	private Dictionary<int, GameObject> _enemies;
	private Dictionary<int, GameObject> _towers;
	private Dictionary<int, Wave> _waves;
	private Dictionary<int, GameObject> _enemieSchedule;
	
	public enum GameState{Menu, Game, Pause, Credits, GameOver, Saving, Loading}
	static public GameState CurrentGameState;
	
	// Use this for initialization
	void Start () {
		CurrentGameState = GameState.Game; // TODO set this someplace else!
		_enemies = new Dictionary<int, GameObject>();
		_towers = new Dictionary<int, GameObject>();
		_waves = new Dictionary<int, Wave>();
		_enemieSchedule = new Dictionary<int, GameObject>();
		_mapManager = new Mapmanager();
		_mapManager.initilize();
		_currentMap = new Map001();
		_mapManager.InitilizeMap(_currentMap);
	}
	
	// Update is called once per frame
	void Update () {
		RandomEnemySpawner();
	}
	
	public Mapmanager MapManager{ get { return _mapManager; } }
	
	public void SpawnEnemy(GameObject EnemyPrefab)
	{
		GameObject go = SpawnPrefab(EnemyPrefab, _currentMap.Waypoints[0]);
		_enemies.Add(go.GetInstanceID(), go);
	}
	
	public void SpawnTower(GameObject TowerPrefab, Vector3 pos)
	{
		GameObject go = SpawnPrefab(TowerPrefab, pos);
		_towers.Add(go.GetInstanceID(), go);
	}
	
	private GameObject SpawnPrefab(GameObject Prefab, Vector3 pos)
	{
		GameObject SpawnedPrefab = Instantiate(Prefab) as GameObject;
		SpawnedPrefab.transform.position = pos;
		return SpawnedPrefab;
	}
	
	public void AddWave(int index, Wave wave)
	{
		_waves.Add(index, wave);
	}
	
	public void AddEnemySchedule(int index, GameObject schedule)
	{
		_enemieSchedule.Add(index, schedule);
	}
	
	
	
	#region RandomEnemySpawner
	private void RandomEnemySpawner()
	{
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > randomSpawnTime) 
		{
			int r = Random.Range(0,3);
			GameObject enemy = Resources.Load("Prefabs/EnemyA0") as GameObject;
			if (r == 1)
				enemy = Resources.Load("Prefabs/EnemyB0") as GameObject;
			else if (r == 2)
				enemy = Resources.Load("Prefabs/EnemyC0") as GameObject;
			if(enemy != null)
			{
				SpawnEnemy(enemy);
			}
			_spawntimedinterval = Time.realtimeSinceStartup;
			randomSpawnTime = Random.Range(1, 5);
		}
	}
	#endregion
}