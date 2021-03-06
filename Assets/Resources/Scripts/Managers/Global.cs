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

    public GameObject mWarning;
    public const int MaxLevels = 10;

    public int LoadedLevel{get{return _loadedlevel;}}


	#region Private Memebers

    private static Global GlobalCreated;
	
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
	private EnemyManager _enemyManager;
	public EggManager eggManager;
	
	[Range (0, 10)]
	public int StartingLevel;
	public float RandomEnemySpawnLow;
	public float RandomEnemySpawnHigh;
	public enum MapToken {Tower = -2, Path = -1, Start = 0}
	public enum GameState{Menu, Game, Pause, Credits, GameOver, Saving, Loading}
	public enum Scenes{Menu = 1, Game = 2}
	static public GameState CurrentGameState;
	public Map CurrentMap { get{ return _currentMap; } }

    public static float SLOW_FACTOR = .5f;
    public int WaveCount { get { return (_currentMap != null) ? _currentMap.getWaveCount() : 999; } }
    public EnemyManager enemyManager{get {
    	if(null == _enemyManager){
    		_enemyManager = new EnemyManager();
    	}
    	return _enemyManager;}}
	#endregion
	
	#region Unity
	
	void Awake()
	{
        //Debug.Log("Global awake");
		_enemyManager = null;
		CurrentGameState = GameState.Menu;
        //Only one global needed
        if (null != GlobalCreated && this != GlobalCreated)
        {
            Destroy(transform.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            GlobalCreated = this;          
        }

        //Not starting from main menu
        if (Application.loadedLevel != 0)
        {
            //LoadLevelLoader(leveltoload);
        }

        LoadSavedGame();

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

        Time.timeScale = 1;
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

    public void Continue()
    {
        int HighestCompletedLevel = SaveLoad.SavedGame.HighestCompletedLevel();
        if (HighestCompletedLevel == 9)
        {
            //Debug.Log("all levels beat, restarting at level 1");
            LoadMap(0);
            Application.LoadLevel(Global.Scenes.Game.ToString());
            return;
        }
        else
        {
            LoadMap(HighestCompletedLevel++);
            Application.LoadLevel(Global.Scenes.Game.ToString());
        }
        return;
    }

    public void ResetGame()
    {
        SaveLoad.Reset();
        LoadMap(0);
        Application.LoadLevel(0);
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
        //mWarning = GameObject.Find("Warning");
        if (eggManager.EggCount == 1)
        {
            //mWarning.SetActive(true);
        }
        else
        {
            //mWarning.SetActive(false);
        }
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
        if (index > MaxLevels)
        {
            Application.LoadLevel(Global.Scenes.Menu.ToString());
            return;
        }
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
		eggManager.SpawnEgg(_currentMap.HitPoints);
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
		_enemyManager = new EnemyManager();
		
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

        Save ();
	}
	
	private void LoseCond()
	{
        Time.timeScale = 0;

        if (OnGameLost != null)
            OnGameLost();
	}
	
	private void Save()
	{
		//Debug.Log ("Saving: " + LoadedLevel + " Unlocked");
		LoadSavedGame();
        SaveLoad.SavedGame.UnlockLevel(LoadedLevel + 1);
		SaveLoad.Save();
	}

    public void unlockalllevels()
    {
        LoadSavedGame();
        SaveLoad.SavedGame.UnlockLevel(9);
        SaveLoad.Save();
    }

    //Lo
    private void LoadSavedGame()
    {
        SaveLoad.Load();
    }
	#endregion
}
