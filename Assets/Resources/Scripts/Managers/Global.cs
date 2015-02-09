using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {

	#region Private Memebers
    private EnemyManager enemyManager;
	private const int STARTING_EGG_COUNT = 10;
	private List<GameObject> _eggsAtGoal;
	private int _eggsStillActive;
	
	private Mapmanager _mapManager;
	private Map _currentMap;
	private Dictionary<int, Map> _maps;
	
	private Dictionary<int, GameObject> _towers;
	
	private bool _mapLoaded = false;
	#endregion
	
	#region Public Memebers
	public GameObject EggPrefab;
	
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
		LoadMap(2);
	}
	
	// Update is called once per frame
	void Update () {
        if (_mapLoaded) 
        {
            enemyManager.StartRandomEnemySpawner();
        }
	}
	#endregion
	
	#region Eggs
	private void InitEggs()
	{
		_eggsAtGoal = new List<GameObject>();
	}
	
	public void SpawnEgg()
	{
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		for(int i = 0; i < STARTING_EGG_COUNT; i++)
		{
			Vector3 endloc = _currentMap.Waypoints[_currentMap.Waypoints.Length - 1];
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
	
	public void DropEgg(Vector3 pos)
	{
		Debug.Log("Dropegg");
		GameObject EggPrefab = Resources.Load("Prefabs/egg") as GameObject;
		GameObject egg = SpawnPrefab(EggPrefab, pos);
		egg.GetComponent<CircleCollider2D>().enabled = true;
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
	
	#region Map
	private void InitMap()
	{
		Dictionary<int, int[,]> m = new GameMaps().GetGameMaps();
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
		SpawnEgg();
		_mapLoaded = true;
	}
	
	private void Initilize()
	{
		CurrentGameState = GameState.Game; // TODO set this someplace else!
        enemyManager = GetComponent<EnemyManager>();
		InitTowers();
		InitMap();
		InitEggs();
		_eggsStillActive = STARTING_EGG_COUNT;
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
		
		if(_towers != null)
		{
			for(int i = 0; i < _towers.Count; i++)
			{
				Destroy(_towers[i]);
			}
		}
	}
	#endregion
	

}
