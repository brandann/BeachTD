using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager{

    #region Public Accessors
    private GameObject grateSpace;
	#endregion
	
	#region Public Methods
	public MapManager()
	{
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
        grateSpace = Resources.Load("Prefabs/GrateSpace") as GameObject;
        global = GameObject.Find("Global").GetComponent<Global>();
	}
	
	public void LoadMap(Map map)
	{
		LoadInitialTowerSpaces(map);
	}
	#endregion
	
	#region Private Members
	private GameObject OpenSpace;
	private Global global;
	#endregion
	
	#region Private Methods
	private void LoadInitialTowerSpaces(Map loadMap)
	{
		bool[,] map = loadMap.TowerLocations();
		
		for (int i = -1; i < map.GetLength(0) + 1; i++)
		{
			for (int j = -1; j < map.GetLength(1) + 1; j++)
			{
                if((i < 0) || (i >= map.GetLength(0)) || (j < 0) || (j >= map.GetLength(1)))
                {
                    makePath(j, i);
                }
                else if (map[i, j])
                {
                    makeSpace(j, i);
                    makePath(j, i);
                }
                else
                    makePath(j, i);
			}
		}
	}
	
	private void makeSpace(int i, int j)
	{
		global.SpawnTower(OpenSpace, new Vector3(i + .5f, j + .5f, 0));
        GameObject SpawnedPrefab = GameObject.Instantiate(grateSpace) as GameObject;
        SpawnedPrefab.transform.position = new Vector3(i + .5f, j + .5f, 0);
    }

    private void makePath(int i, int j)
    {
        TowerFactory.Instance.CreatePathSquare().transform.position = new Vector3(i + .5f, j + .5f, 0);
    }
	#endregion
}
