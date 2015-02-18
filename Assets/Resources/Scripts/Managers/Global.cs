using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour {
	
    public StatusBar StatBar;
    public GameObject PauseScreen;
    public GameObject PauseButton;

	#region Private Memebers
    //private EnemyManager enemyManager;
/*	private const int STARTING_EGG_COUNT = 10;
	private List<GameObject> _eggsAtGoal;


	private int _eggsStillActive;*/
	
	private MapManager _mapManager;
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
	public Map CurrentMap { get{ return _currentMap; } }
	#endregion
	
	#region Unity
	// Use this for initialization
	void Start () {
		LoadMap(StartingLevel);

        if (StatBar == null || PauseButton == null || PauseScreen == null)
            Debug.LogError("missing UI ref");
        else
        {
            PauseButton.SetActive(true);
            PauseScreen.SetActive(false);
        }

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
	
	#region Public Methods
	public GameObject SpawnPrefab(GameObject Prefab, Vector3 pos)
	{
		GameObject SpawnedPrefab = Instantiate(Prefab) as GameObject;
		SpawnedPrefab.transform.position = pos;
		return SpawnedPrefab;
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        PauseButton.SetActive(true);
    }
	#endregion
	
	#region Private Methods
	private void LoadMap(int index)
	{
		Initilize();
		
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
		_maps = new Dictionary<int, Map>();
		List<Map> tempMaps = LoadMaps();
		
		for(int i = 0; i < tempMaps.Count; i++)
		{
			_maps.Add(i, tempMaps[i]);
		}
		
		_mapManager = new MapManager();
		
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
	
	private List<Map> LoadMaps()
	{
		List<Map> m = new List<Map>();
		
		m.Add(new Map00().GetMap());
		m.Add(new Map01().GetMap());
		m.Add(new Map02().GetMap());
		m.Add(new Map03().GetMap());
		m.Add(new Map04().GetMap());
		m.Add(new Map05().GetMap());
		m.Add(new Map06().GetMap());
		m.Add(new Map07().GetMap());
		m.Add(new Map08().GetMap());
		m.Add(new Map09().GetMap());
		m.Add(new Map10().GetMap());
		m.Add(new Map11().GetMap());
		m.Add(new Map12().GetMap());
		m.Add(new Map13().GetMap());
		m.Add(new Map14().GetMap());
		m.Add(new Map15().GetMap());
		m.Add(new Map16().GetMap());
		m.Add(new Map17().GetMap());
		m.Add(new Map18().GetMap());
		m.Add(new Map19().GetMap());
		
		
		return m;
	}
	#endregion
}
