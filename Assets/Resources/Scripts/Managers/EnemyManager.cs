using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase {

	#region Private memebers
	private List<Wave> _waves;
	
    private Wave _currentWave;
    private int _currentWaveIndex;
    private int _waveIndex;
    private float delay;
    
    private GameObject EnemyA0Prefab;
    private GameObject EnemyB0Prefab;
    private GameObject EnemyC0Prefab;

    private float _spawntimedinterval = 0;
    private float randomSpawnTime = 0;

    private bool _mapLoaded = false;
    private bool _winable = false;
    #endregion
	
	#region Public methods
	// Use this for initialization
    public EnemyManager() : base()
    {
		EnemyA0Prefab = Resources.Load("Prefabs/EnemyA0") as GameObject;
		EnemyB0Prefab = Resources.Load("Prefabs/EnemyB0") as GameObject;
		EnemyC0Prefab = Resources.Load("Prefabs/EnemyC0") as GameObject;
    }
	
	// Update is called once per frame
	public void Update () {
		LoadWaveEnemy();
        //RandomEnemySpawner(_mapLoaded);

        if (Input.GetKeyUp(KeyCode.Alpha1)) { Create(EnemyA0Prefab, _startingPosition); }
     	else if (Input.GetKeyUp(KeyCode.Alpha2)) { Create(EnemyB0Prefab, _startingPosition); }
  		else if (Input.GetKeyUp(KeyCode.Alpha3)) { Create(EnemyC0Prefab, _startingPosition); }
	}

    public void StartRandomEnemySpawner()
    {
        _mapLoaded = true;
    }
    
    public void SetWaves(List<Wave> waves)
    {
    	_waves = waves;
    	if(_waves.Count > 0)
    	{
    		_currentWave = _waves[0];
    	}
    	else
    	{
    		_waves = null;
    	}
		_currentWaveIndex = 0;
		_waveIndex = 0;
    }
	#endregion
	
	#region Private Methods
	private void LoadWaveEnemy()
	{
		if(_winable && GetActiveCount() == 0)
		{
			Application.LoadLevel(Global.Scenes.Menu.ToString());
		}
		if(_waves == null)
		{
			return;
		}
		
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > delay)
		{
			switch(_currentWave.GetScheduleItem(_currentWaveIndex).token)
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
			
			_currentWaveIndex++;
			
			if(_currentWaveIndex == _currentWave.Count())
			{
				// end of wave
				_currentWaveIndex = 0;
				
				
				_waveIndex++;
				
				if(_waveIndex == _waves.Count)
				{
					// end of all waves
					_waves = null;
					_winable = true;
					return;
				}
				
				_currentWave = _waves[_waveIndex];
			}
			
			delay = _currentWave.GetScheduleItem(_currentWaveIndex).time;
			_spawntimedinterval = Time.realtimeSinceStartup;
		}
	}
	
    private void RandomEnemySpawner(bool go)
    {
        if (!go) return;

        if ((Time.time - _spawntimedinterval) > randomSpawnTime)
        {
            int r = Random.Range(0, 3);
            GameObject enemy = Resources.Load("Prefabs/EnemyA0") as GameObject;
            if (r == 1)
                enemy = Resources.Load("Prefabs/EnemyB0") as GameObject;
            else if (r == 2)
                enemy = Resources.Load("Prefabs/EnemyC0") as GameObject;
            if (enemy != null)
            {
                Create(enemy, _startingPosition);
            }
            _spawntimedinterval = Time.realtimeSinceStartup;
            //randomSpawnTime = Random.Range(RandomEnemySpawnLow, RandomEnemySpawnHigh);
            randomSpawnTime = 3;
        }
    }
    #endregion
}
