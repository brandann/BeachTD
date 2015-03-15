using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase {

	#region Private memebers
	private Queue<Wave> _waveQueue;
	private Queue<EnemySchedule> _enemyQueue;
	
    private float delay;
    
    private GameObject EnemyA0Prefab;
    private GameObject EnemyB0Prefab;
    private GameObject EnemyC0Prefab;

    private float _spawntimedinterval = 0;
    
    private ManagerState _currentManagerState;
    #endregion
    
    #region Public Members
	public enum ManagerState {Prep, Active, Done, WaitForPrevWave};
    public ManagerState CurrentManagerState { get { return _currentManagerState; } }
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

        if (Input.GetKeyUp(KeyCode.Alpha1)) { Create(EnemyA0Prefab, _startingPosition); }
     	else if (Input.GetKeyUp(KeyCode.Alpha2)) { Create(EnemyB0Prefab, _startingPosition); }
  		else if (Input.GetKeyUp(KeyCode.Alpha3)) { Create(EnemyC0Prefab, _startingPosition); }
	}
    
    public void SetWaves(List<Wave> waves)
    {
    	_waveQueue = new Queue<Wave>();
    	_enemyQueue = new Queue<EnemySchedule>();
    	
    	_currentManagerState = ManagerState.Active;
    	
    	for (int i = 0; i < waves.Count; i++)
    	{
    		_waveQueue.Enqueue(waves[i]);
    	}
    	
    	SetNextWave();
    }
	#endregion
	
	#region Private Methods
	private bool SetNextWave()
	{	
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
			delay = _enemyQueue.Peek().time;
			_spawntimedinterval = Time.realtimeSinceStartup;
			if(_enemyQueue.Peek().token == EnemySchedule.Token.WAIT)
			{
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
				if ((Time.realtimeSinceStartup - _spawntimedinterval) > delay)
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
					_spawntimedinterval = Time.realtimeSinceStartup;
				}
				break;
		}
	}
    #endregion
}
