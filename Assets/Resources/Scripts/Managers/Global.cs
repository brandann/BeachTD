using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {
	
    public StatusBar StatBar;

	#region Private Memebers
    //private EnemyManager enemyManager;
/*	private const int STARTING_EGG_COUNT = 10;
	private List<GameObject> _eggsAtGoal;


	private int _eggsStillActive;*/
	
	private Mapmanager _mapManager;
	private Map _currentMap;
	private Dictionary<int, Map> _maps;

	private Dictionary<int, GameObject> _towers;
	
	private bool _mapLoaded = false;
	#endregion
	
	#region Public Memebers
	public GameObject EggPrefab;
	public EnemyManager enemyManager;
	public EggManager eggManager;
	
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
		LoadMap(StartingLevel);

        if (StatBar == null)
            Debug.LogError("missing status Bar ref");
	}
	
	// Update is called once per frame
	void Update () {
		int EnemyCount;
		int WaveCount;	
		
		if(enemyManager != null)
		{
			enemyManager.Update();
		}
        if (_mapLoaded) 
        {
            enemyManager.StartRandomEnemySpawner();
        }
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
	
	#region Map
	private void InitMap()
	{
		
	}

	
	public Map CurrentMap
	{
		get{ return _currentMap; }
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
	private void LoadMap(int index)
	{
		_mapManager.LoadMap(_maps[index]);
		_currentMap = _maps[index];
		eggManager.SpawnEgg();
		_mapLoaded = true;
		enemyManager.SetStartingPosition(CurrentMap.Waypoints[0]);
	}
	
	private void Initilize()
	{
		CurrentGameState = GameState.Game; // TODO set this someplace else!
        
		// Towers ------------------------------------------------------------
		InitTowers();
		
		// Maps ------------------------------------------------------------
		Dictionary<int, int[,]> m = new GameMap().GetGameMaps();
		if(_maps != null)
		{
			_maps.Clear();
		}
		_maps = new Dictionary<int, Map>();
		
		for(int i = 0; i < m.Count; i++)
		{
			_maps.Add(i, new Map(m[i]));
		}
		
		_mapManager = new Mapmanager();
		
		// Enemies ------------------------------------------------------------
		enemyManager = new EnemyManager();
		
		// Eggs ------------------------------------------------------------
		eggManager = new EggManager();
	}
	
	private void Reset()
	{
		if(_towers != null)
		{
			for(int i = 0; i < _towers.Count; i++)
			{
				Destroy(_towers[i]);
			}
		}
	}
	
	// this will be the pause delegate at some point...
	private void Pause(bool pause)
	{
		gameObject.SetActive(pause);
	}
	#endregion
}
