using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mapmanager{

	#region Public Accessors
	public Map CurrentMap
	{
		get{ return _currentMap; }
	}
	#endregion
	
	#region Public Methods
	public Mapmanager()
	{
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		global = GameObject.Find("Global").GetComponent<Global>();
		InitilizeMaps();
	}
	
	public void LoadMap(int i)
	{
		LoadInitialTowerSpaces(_maps[i]);
		_currentMap = _maps[i];
	}
	#endregion
	
	#region Private Members
	private GameObject OpenSpace;
	private Global global;
	private Map _currentMap;
	private Dictionary<int, Map> _maps;
	#endregion
	
	#region Private Methods
	private void LoadInitialTowerSpaces(Map loadMap)
	{
		_currentMap = loadMap;
		bool[,] map = loadMap.BoolMap;
		
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i,j])
				{
					makeSpace(j, i);
				}
			}
		}
	}
	
	private void makeSpace(int i, int j)
	{
		global.SpawnTower(OpenSpace, new Vector3(i + .5f, j + .5f, 0));
	}
	#endregion

	#region Maps
	private void InitilizeMaps()
	{
		_maps = GameMaps.GetGameMaps();
	}
	#endregion
}
