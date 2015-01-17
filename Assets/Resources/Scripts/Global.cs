﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {

	private GameMaps _currentMap;
	private Mapmanager _mapManager;
	private float _spawntimedinterval = 0;
	private float randomSpawnTime = 0;
	private Dictionary<int, GameObject> _enemies;
	private Dictionary<int, GameObject> _towers;
	private Dictionary<int, Wave> _waves;
	private Dictionary<int, GameObject> _enemieSchedule;
	private List<GameObject> _eggs;
	
	public GameObject EggPrefab;
	
	public enum MapToken {Tower = -2, Path = -1, Start = 0}
	
	public enum GameState{Menu, Game, Pause, Credits, GameOver, Saving, Loading}
	static public GameState CurrentGameState;
	
	// Use this for initialization
	void Start () {
		CurrentGameState = GameState.Game; // TODO set this someplace else!
		_enemies = new Dictionary<int, GameObject>();
		_towers = new Dictionary<int, GameObject>();
		_waves = new Dictionary<int, Wave>();
		_enemieSchedule = new Dictionary<int, GameObject>();
		_eggs = new List<GameObject>();
		_mapManager = new Mapmanager();
		_mapManager.LoadMap(5);
		SpawnEgg();
	}
	
	// Update is called once per frame
	void Update () {
		RandomEnemySpawner();
	}
	
	public Mapmanager MapManager{ get { return _mapManager; } }
	
	public void SpawnEnemy(GameObject EnemyPrefab)
	{
		GameObject go = SpawnPrefab(EnemyPrefab, _mapManager.CurrentMap.Waypoints[0]);
		_enemies.Add(go.GetInstanceID(), go);
	}
	
	public void SpawnTower(GameObject TowerPrefab, Vector3 pos)
	{
		GameObject go = SpawnPrefab(TowerPrefab, pos);
		_towers.Add(go.GetInstanceID(), go);
	}
	
	public void SpawnEgg()
	{
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		for(int i = 0; i < 5; i++)
		{
			GameObject egg = SpawnPrefab(EggPrefab, new Vector3(Random.Range(12, 12.5f),Random.Range(4f,4.5f), 0));
			_eggs.Add(egg);
		}
	}
	
	private int eggindex = 0;
	
	public void GiveEggToEnemy(GameObject go)
	{
		if(_eggs.Count > 0)
		{
			GameObject egg = _eggs[0];
			_eggs.RemoveAt(0);
			//egg.transform.position = new Vector3(0,.5f,0);
			egg.transform.localScale = new Vector3(.5f, .5f, .5f);
			egg.transform.parent = go.transform;
			egg.transform.position = go.transform.position;
			egg.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -1);
			go.GetComponent<Enemy>().HasEgg = true;
			
		}
	}
	
	public GameObject SpawnPrefab(GameObject Prefab, Vector3 pos)
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
	
	public Map CurrentMap
	{
		get { return MapManager.CurrentMap; }
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
			randomSpawnTime = Random.Range(1, 20);
		}
	}
	#endregion
}
