using UnityEngine;
using System.Collections;

public class Mapmanager : MonoBehaviour {

	Map currentMap;
	GameObject OpenSpace;
	private float scale = .5f;
	
	
	// Use this for initialization
	void Start () {
		
		// load the OpenSpace prefab
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		
		// make level 1 on default
		// this is for testing and should be
		// removed later when we dont want a map created right away!
		makeMap1();
		
		// build the OpenSapces for Map1
		// this is for testing and should be
		// removed later when we dont want a map created right away!
		makeOpenSpaces();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetAxis("Fire1") > 0f)
		{
			GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
			GameObject e = Instantiate(enemy) as GameObject;
		}
	}
	
	// Make OpenSpace traverses trough currentMap's boolean map to
	// find locations for OpenSpace's
	private void makeOpenSpaces()
	{
	/*
		// get the currentMaps map to traverse
		bool [,] map = currentMap.getMap();
		
		// get the camera for size's
		Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		*/
	///*
		// get the currentMaps map to traverse
		bool [,] map = currentMap.getMap();
		
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
		
		//*/
	}
	
	// Make and set new OpenSpace
	// Openspaces are used to make a buildable area for a tower
	// use i and j to find the position of the Openspace to be built.
	// use camera to get the camera size and orthographicSize
	private void makeSpace(int i, int j, Camera camera)
	{
	
	
		// get the camera width
		float cameraWidth = camera.orthographicSize * camera.aspect;
		
		// use the camera size to set the openspace position
		float x = (0 - cameraWidth) + (i * scale) + (scale/2);
		float y = (0 - camera.orthographicSize) + (j * scale) + (scale/2);
		
		// instantiate and set set position for new OpenSpace
		GameObject e = Instantiate(OpenSpace) as GameObject;
		e.transform.position = new Vector3(x, y, 0);
		
		
	}
	
	public Vector2[] getWaypoints() {
		return currentMap.getWaypoints();
	}
	
	public float getScale()
	{
		return scale;
	}
	
	//
	// --------------------------------------------------------------------
	// make hard coded maps
	// --------------------------------------------------------------------
	//
	
	private bool[,] fixMap(bool[,] m)
	{
		for (int h = 0; h < m.GetLength(0); h++)//height
		{
			for (int j = (m.GetLength(0) -1) -h; j >= 0; j--)//height
			{
				for (int l = 0; l < Level1.GetLength(1); l++)
				{
					TempArr[j, l] = Level1[h, l];
				}
			}
		}
	}
	
	
	
	// Make the Map class for Map level 1
	// map is created with a 2d boolean array and corisponding
	// way points. true spaces are where towers can be built,
	// false are unbildable spaces/pathways
	//
	// could be changed to a text file for easyier change. hard code == bad
	private void makeMap1()
	{
		// temp bool variables for readability only
		// remove for final to minimize work
		bool I = true;
		bool O = false;
		
		// map array to make Map class with
		bool[,] map = new bool[,]
		{
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	O,	O,	O,	O,	O,	O,	O	},
			{	I,	I,	I,	I,	I,	O,	I,	I,	I,	I,	I,	I	},
			{	O,	O,	O,	O,	O,	O,	I,	I,	I,	I,	I,	I	},
			{	O,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
		};
		
		for(int i = 0; i < map.GetLength(1); i++)
		{
			
		}
		
		
		
		// way points to be passed to enimies for map
		// traversal. can be a different type for simplier
		// reversal for journey back.
		Vector2[] waypoints = new Vector2[]
		{
			new Vector3(0,4),
			new Vector2(5,4),
			new Vector2(5,2),
            new Vector2(2,11)
		};
		
		
		// create new Map() and set to current map
		currentMap = new Map();
		
		// give currentMap the map and waypoint information to be accessed
		// for future use and enemy support
		currentMap.setMap(map,waypoints);
	}
}
