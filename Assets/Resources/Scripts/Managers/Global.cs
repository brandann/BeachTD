﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {

	#region Private Memebers
	private const int STARTING_EGG_COUNT = 10;
	private GameMaps _currentMap;
	private Mapmanager _mapManager;
	private float _spawntimedinterval = 0;
	private float randomSpawnTime = 0;
	private Dictionary<int, GameObject> _enemies;
	private Dictionary<int, GameObject> _towers;
	private Dictionary<int, Wave> _waves;
	private Dictionary<int, GameObject> _enemieSchedule;
	private List<GameObject> _eggsAtGoal;
	private int _eggsStillActive;
	#endregion
	
	#region Public Memebers
	public GameObject EggPrefab;
	public GameObject EnemyA0Prefab;
	public GameObject EnemyB0Prefab;
	public GameObject EnemyC0Prefab;
	
	[Range (0, 10)]
	public int StartingLevel;
	public float RandomEnemySpawnLow;
	public float RandomEnemySpawnHigh;
	public enum MapToken {Tower = -2, Path = -1, Start = 0}
	public enum GameState{Menu, Game, Pause, Credits, GameOver, Saving, Loading}
	static public GameState CurrentGameState;
	#endregion
	
	#region Unity
	// Use this for initialization
	void Start () {
		Initilize();
	}
	
	// Update is called once per frame
	void Update () {
		RandomEnemySpawner();
		
		if(Input.GetKeyUp(KeyCode.Alpha1)) { SpawnEnemy(EnemyA0Prefab); }
		else if(Input.GetKeyUp(KeyCode.Alpha2)) { SpawnEnemy(EnemyB0Prefab); }
		else if(Input.GetKeyUp(KeyCode.Alpha3)) { SpawnEnemy(EnemyC0Prefab); }
	}
	#endregion
	
	#region Eggs
	private void InitEggs()
	{
		_eggsAtGoal = new List<GameObject>();
		
		SpawnEgg();
	}
	
	public void SpawnEgg()
	{
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		for(int i = 0; i < STARTING_EGG_COUNT; i++)
		{
			Vector3 endloc = MapManager.CurrentMap.Waypoints[MapManager.CurrentMap.Waypoints.Length - 1];
			Vector3 randloc = new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-1.5f,1.5f), 0);
			Vector3 offset = new Vector3(1,0,0);
			GameObject egg = SpawnPrefab(EggPrefab, endloc + randloc + offset);
			_eggsAtGoal.Add(egg);
		}
	}
	
	public void GetEggFromPath()
	{
			
	}
	
	public GameObject GetEggFromGoal()
	{
		if(_eggsAtGoal.Count > 0)
		{
			GameObject egg = _eggsAtGoal[0];
			_eggsAtGoal.RemoveAt(0);
			
			return egg;
		}
		return null;
	}
	
	public void DropEgg()
	{
		
	}
	
	public void DestroyEgg()
	{
		_eggsStillActive--;
	}
	#endregion
	
	#region Towers
	private void InitTowers()
	{
		_towers = new Dictionary<int, GameObject>();
	}
	
	public void SpawnTower(GameObject TowerPrefab, Vector3 pos)
	{
		GameObject go = SpawnPrefab(TowerPrefab, pos);
		_towers.Add(go.GetInstanceID(), go);
	}
	#endregion
	
	#region Enemies
	private void InitEnemies()
	{
		_enemies = new Dictionary<int, GameObject>();
		_waves = new Dictionary<int, Wave>();
		_enemieSchedule = new Dictionary<int, GameObject>();
		
		EnemyA0Prefab = Resources.Load("Prefabs/EnemyA0") as GameObject;
		EnemyB0Prefab = Resources.Load("Prefabs/EnemyB0") as GameObject;
		EnemyC0Prefab = Resources.Load("Prefabs/EnemyC0") as GameObject;
	}
	
	public void SpawnEnemy(GameObject EnemyPrefab)
	{
		GameObject go = SpawnPrefab(EnemyPrefab, _mapManager.CurrentMap.Waypoints[0]);
		_enemies.Add(go.GetInstanceID(), go);
	}
	
	public void AddEnemySchedule(int index, GameObject schedule)
	{
		_enemieSchedule.Add(index, schedule);
	}
	
	public void AddWave(int index, Wave wave)
	{
		_waves.Add(index, wave);
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
			randomSpawnTime = Random.Range(RandomEnemySpawnLow, RandomEnemySpawnHigh);
		}
	}
	#endregion
	#endregion
	
	#region Map
	private void InitMap()
	{
		_mapManager = new Mapmanager();
		_mapManager.LoadMap(StartingLevel);
	}
	
	public Mapmanager MapManager
	{ 
		get { return _mapManager; }
	}
	
	public Map CurrentMap
	{
		get { return MapManager.CurrentMap; }
	}
	#endregion
	
	#region Public Methods
	public GameObject SpawnPrefab(GameObject Prefab, Vector3 pos)
	{
		GameObject SpawnedPrefab = Instantiate(Prefab) as GameObject;
		SpawnedPrefab.transform.position = pos;
		return SpawnedPrefab;
	}
	#endregion
	
	#region Private Methods
	private void Initilize()
	{
		Reset();
	}
	
	private void Reset()
	{
		if(_eggsAtGoal != null)
		{
			for(int i = 0; i < _eggsAtGoal.Count; i++)
			{
				Destroy(_eggsAtGoal[i]);
			}
		}
		
		if(_enemies != null)
		{
			for(int i = 0; i < _enemies.Count; i++)
			{
				Destroy(_enemies[i]);
			}
		}
		
		if(_towers != null)
		{
			for(int i = 0; i < _towers.Count; i++)
			{
				Destroy(_towers[i]);
			}
		}
		
		if(_waves != null)
		{
			for(int i = 0; i < _waves.Count; i++)
			{
				Destroy(_waves[i]);
			}
		}
		
		
		CurrentGameState = GameState.Game; // TODO set this someplace else!
		InitTowers();
		InitMap();
		InitEnemies();
		InitEggs();
		_eggsStillActive = STARTING_EGG_COUNT;
	}
	#endregion
	

}
