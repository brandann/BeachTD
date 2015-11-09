using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase
{
    #region events
    public delegate void WaveStarted();
    public static event WaveStarted OnWaveStarted;

    #endregion

    #region Private memebers
    private Queue<Wave> _waveQueue;
	private Queue<EnemySchedule> _enemyQueue;
	
    private float delay;
    private float delay_multiplyer = 4;
    
    private GameObject EnemyA0Prefab;
    private GameObject EnemyB0Prefab;
    private GameObject EnemyC0Prefab;

    private float _spawntimedinterval = 0;
    
    private ManagerState _currentManagerState;

    #endregion
    
    #region Public Members
	public enum ManagerState {Prep, Active, Done, WaitForPrevWave, WaitForTowers};
    public ManagerState CurrentManagerState { get { return _currentManagerState; } }
    public int WaveCount { get; protected set; }
    #endregion
	
	#region Public methods
	// Use this for initialization
    public EnemyManager() : base()
    {
    	_currentManagerState = ManagerState.Prep;
		EnemyA0Prefab = Resources.Load("Prefabs/EnemyA0") as GameObject;
		EnemyB0Prefab = Resources.Load("Prefabs/EnemyB0") as GameObject;
		EnemyC0Prefab = Resources.Load("Prefabs/EnemyC0") as GameObject;
    }
	
	// Update is called once per frame
	public void Update () {
	
		if(Global.CurrentGameState != Global.GameState.Game)
		{
			return;
		}
		
		LoadWaveEnemy();

        //if (Input.GetKeyUp(KeyCode.Alpha1)) { Create(EnemyA0Prefab, _startingPosition); }
     	//else if (Input.GetKeyUp(KeyCode.Alpha2)) { Create(EnemyB0Prefab, _startingPosition); }
  		//else if (Input.GetKeyUp(KeyCode.Alpha3)) { Create(EnemyC0Prefab, _startingPosition); }
	}
    
    public void NotifyTowerBuilt()
    {
    	if (_currentManagerState == ManagerState.WaitForTowers)
    	{
    		_currentManagerState = ManagerState.Active;
    	}
    }
    
    public void SetWaves(List<Wave> waves)
    {
    	_waveQueue = new Queue<Wave>();
    	_enemyQueue = new Queue<EnemySchedule>();
    	
    	_currentManagerState = ManagerState.Active;
        WaveCount = waves.Count;

    	for (int i = 0; i < waves.Count; i++)
    	{
    		_waveQueue.Enqueue(waves[i]);
    	}
    	
    	SetNextWave();
    	
		_currentManagerState = ManagerState.WaitForTowers;
	}
	#endregion
	
	#region Private Methods
	private bool SetNextWave()
	{
        //if (OnWaveStarted != null)
        //{
        //    OnWaveStarted();
        //    Debug.Log("wave triggered");
        //}

		if(_waveQueue == null || _waveQueue.Count == 0)
		{
			_currentManagerState = ManagerState.Done;
			_waveQueue = null;
			_enemyQueue = null;            
            
			return false;
		}

		Wave nextWave = _waveQueue.Dequeue();
		_enemyQueue = new Queue<EnemySchedule>();
		
		for(int i = 0; i < nextWave.Count(); i++)
		{
			_enemyQueue.Enqueue(nextWave.GetScheduleItem(i));
		}
		
		ResetTime();
		
		return true;
	}
	
	private void ResetTime()
	{
		if(_enemyQueue != null && _enemyQueue.Count > 0)
		{
            delay = _enemyQueue.Peek().time * delay_multiplyer;
			_spawntimedinterval = Time.time;
			if(_enemyQueue.Peek().token == EnemySchedule.Token.WAIT)
			{
                delay *= 2;
				_currentManagerState = ManagerState.WaitForPrevWave;
			}
		}
	}
	
	private void SpawnEnemy(EnemySchedule.Token token)
	{
		switch(token)
		{
		case(EnemySchedule.Token.WAIT):
			break;
		case(EnemySchedule.Token.A0):
			Create(EnemyA0Prefab, _startingPosition);
			break;
		case(EnemySchedule.Token.A1):
			break;
		case(EnemySchedule.Token.B0):
			Create(EnemyB0Prefab, _startingPosition);
			break;
		case(EnemySchedule.Token.B1):
			break;
		case(EnemySchedule.Token.C0):
			Create(EnemyC0Prefab, _startingPosition);
			break;
		case(EnemySchedule.Token.C1):
			break;
		}
	}
	
	private void LoadWaveEnemy()
	{
		switch(_currentManagerState)
		{
			case(ManagerState.Prep):
				return;
			case(ManagerState.Active):
				if ((Time.time - _spawntimedinterval) > delay)
				{
					EnemySchedule es = _enemyQueue.Dequeue();
					SpawnEnemy(es.token);
					
					if(_enemyQueue.Count == 0)
					{
						SetNextWave();
					}
					ResetTime();
				}
				break;
			case(ManagerState.Done):
				return;
			case(ManagerState.WaitForPrevWave):
				if(GetActiveCount() == 0)
				{
					_currentManagerState = ManagerState.Active;
					_spawntimedinterval = Time.time;

                    if (OnWaveStarted != null)
                    {
                        OnWaveStarted();
                        Debug.Log("wave triggered");
                    }
				}
				break;
			case(ManagerState.WaitForTowers):
				return;
		}
	}
    #endregion
}
