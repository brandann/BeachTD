using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{

    #region Events

    public delegate void GameStateChanged();
    public static event GameStateChanged OnGamePaused;
    public static event GameStateChanged OnGameResumed;
    public static event GameStateChanged OnGameWon;
    public static event GameStateChanged OnGameLost;

    #endregion

  
    public const int MaxLevels = 25;

    public int LoadedLevel{get{return _loadedlevel;}}


	#region Private Memebers
    //private EnemyManager enemyManager;
/*	private const int STARTING_EGG_COUNT = 10;
	private List<GameObject> _eggsAtGoal;


	private int _eggsStillActive;*/
    private static bool GlobalCreated;
	
	private MapManager _mapManager;
	private Map _currentMap;
	private Dictionary<int, Map> _maps;

	private Dictionary<int, GameObject> _towers;
	
	private enum WinStatus{Win, Neutral, Lose}
	private int leveltoload = 0;
	private int _loadedlevel;
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
	public enum Scenes{Menu, Game, Levels}
	static public GameState CurrentGameState;
	public Map CurrentMap { get{ return _currentMap; } }
	#endregion
	
	#region Unity
	
	void Awake()
	{
		CurrentGameState = GameState.Menu;
        //Only one global needed
        if (GlobalCreated)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(this);
            GlobalCreated = true;          
        }

        //Not starting from main menu
        if (Application.loadedLevel != 0)
        {
            //LoadLevelLoader(leveltoload);
        }			

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(enemyManager != null)
		{
			enemyManager.Update();
		}
	}

    void OnLevelWasLoaded(int lvl)
    {
        if (Application.loadedLevelName == "Game" )
        {
            LoadLevelLoader(leveltoload);  
			eggManager.BroadcastEggCount();         
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
        
        
        //Inform subscribers of pause event
        if (OnGamePaused != null)
            OnGamePaused();
    }

    public void ResumeGame()
    {        
        Time.timeScale = 1;        

        //Inform subscribers of resume event
        if (OnGameResumed != null)
            OnGameResumed();
    }
    
    public void WinLoseCond()
    {
		if(eggManager.EggCount == 0)
		{
			LoseCond();
		}
    	else if(enemyManager.GetActiveCount() == 0 && enemyManager.CurrentManagerState == EnemyManager.ManagerState.Done)
    	{
    		WinCond();
    	}
    }
	public void LoadMap(int index)
	{
		leveltoload = index;
		_loadedlevel = index;
	}
	#endregion
	
	#region Private Methods
	private void LoadLevelLoader(int index)
	{
		Initilize();
		
		_mapManager.LoadMap(_maps[index]);
		_currentMap = _maps[index];
		eggManager.SpawnEgg();
		enemyManager.SetStartingPosition(CurrentMap.Waypoints[0]);
		enemyManager.SetWaves(_currentMap.GetWaves());
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

        Time.timeScale = 1;
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
	
	private void WinCond()
	{
        Time.timeScale = 0;

        if (OnGameWon != null)
            OnGameWon();
	}
	
	private void LoseCond()
	{
        Time.timeScale = 0;

        if (OnGameLost != null)
            OnGameLost();
	}
	#endregion
}
