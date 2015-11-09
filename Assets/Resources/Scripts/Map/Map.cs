using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {

	#region Public Methods
	public Map(int[,] map)
	{
		_map = map;
		_waypoints = FindWayPoints(_map);
		waves = new List<Wave>();
	}
	
	public bool[,] TowerLocations()
	{
		bool[,] b = new bool[_map.GetLength(0), _map.GetLength(1)];
		for(int i = 0; i < _map.GetLength(0); i++)
		{
			for(int j = 0; j < _map.GetLength(1); j++)
			{
				b[i,j] = _map[i,j] == (int) Global.MapToken.Tower;
			}
		}
		return b;
	}
	
	public void AddWave(Wave wave)
	{
		waves.Add(wave);
	}
	
	public List<Wave> GetWaves()
	{
		return waves;
	}
	
	public Wave GetWaveByIndex(int index)
	{
		if(index < 0 || index > waves.Count)
		{
			return null;
		}
		return waves[index];
	}
	#endregion
	
	#region public accessors
	public int[,] MapArray
	{
		get{ return _map; }
	}
	
	public Vector3[] Waypoints
	{
		get{ return _waypoints; }
	}
	
	public int HitPoints
	{
		get{ return _hitpoints; }
	}
	#endregion
	
	#region private members
	protected List<Wave>	waves;
	protected int[,]		_map;		// int array of map w/ paths
	protected Vector3[] 	_waypoints;	// enemys path for map
	protected int			_hitpoints = 10;	// "lives" for level
    protected int A = 'a';
    protected int B = 'b';
    protected int C = 'c';
	#endregion

    public void setWaves(int[,] waves)
    {
        for (int i = 0; i < waves.GetLength(0); i++)
        {
            // create new wave and add a wait of 0
            Wave wave = new Wave();
            wave.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.WAIT, 0f));

            // add eneimes
            for (int j = 0; j < waves.GetLength(1); j++)
            {
                if (waves[i, j] == A)
                {
                    wave.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.A0, 1f - (i * 0.07f)));
                }
                else if (waves[i, j] == B)
                {
                    wave.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.B0, 1f - (i * 0.05f)));
                }
                else if (waves[i, j] == C)
                {
                    wave.AddScheduleItem(new EnemySchedule(EnemySchedule.Token.C0, 1f - (i * 0.04f)));
                }
            }
            // add wave to map
            AddWave(wave);
        }
    }
	
	#region Private Methods
	private Vector3[] FindWayPoints(int[,] m)
	{
		int MaxWaypoint = 0;
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] > MaxWaypoint)
				{
					MaxWaypoint = m[i,j];
				}
			}
		}
		Vector3[] FoundWaypoints = new Vector3[MaxWaypoint+1];
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] >= 0)
				{
					FoundWaypoints[m[i,j]] = new Vector3(j + .5f, i + .5f, 0);
				}
			}
		}
        FoundWaypoints[0].x -= 1;
		return FoundWaypoints;
	}
	
	private Vector3 FindPointByIndex(int index, int[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for(int j = 0; j < m.GetLength(1); j++)
			{
				if(m[i,j] == index)
				{
					return new Vector3(i, j, 0);
				}
			}
		}
		return new Vector3(-1,-1,-1);
	}
	
	// Mirror the map verically so bool array matches
	// screen map.
	private int[,] FixMap(int[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				int temp = m[i,j];
				m[i,j] = m[m.GetLength(0) -i -1, j];
				m[i=m.GetLength(0) -i -1,j] = temp;
			}
			
			// if i is >= m.getLength(0) -i -1 then the map has been mirrored to
			// the half point and needs to be returned.
			// if the map continues to mirror it will mirrow back to its
			// origianl layout
			if( (i >= m.GetLength(0) -i) )
			{
				return m;
			}
		}
		Debug.LogError("Map Fix is Bad");
		return m; // should never reach this
	}
	#endregion
}
