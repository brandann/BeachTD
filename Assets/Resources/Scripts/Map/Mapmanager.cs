using UnityEngine;
using System.Collections;

public class Mapmanager{

	private GameObject OpenSpace;
	private Global global;
	private LevelMap _currentMap;

	
	public Mapmanager()
	{
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		global = GameObject.Find("Global").GetComponent<Global>();
	}

	public void initilize()
	{
		
	}
	
	// Make OpenSpace traverses trough currentMap's boolean map to
	// find locations for OpenSpace's
	public void InitilizeMap(LevelMap loadMap)
	{
		_currentMap = loadMap;
		bool [,] map = loadMap.Map;
		
		// get the camera for size's
		Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		// traverse the map and call 'makeSpace()' to create all OpenSpaces
		// at applicailbe locations
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i,j])
				{
					makeSpace(j, i, camera);
				}
			}
		}
	}
	
	// Make and set new OpenSpace
	// Openspaces are used to make a buildable area for a tower
	// use i and j to find the position of the Openspace to be built.
	// use camera to get the camera size and orthographicSize
	private void makeSpace(int i, int j, Camera camera)
	{
		//float cameraWidth = camera.orthographicSize * camera.aspect;
		//float x = (i * _scale) + (_scale/2);
		//float y = (j * _scale) + (_scale/2);
		
		global.SpawnTower(OpenSpace, new Vector3(i + .5f, j + .5f, 0));
	}
	
	#region Public Accessors
	// return the current maps waypoints for enemys to traverse
	public Vector3[] WayPoints
	{
		get{ return _currentMap.Waypoints; }
	}

	public Vector3 StartingPosition
	{
		get
		{ 
			return _currentMap.Waypoints[0];
		}
	}
	#endregion
}
