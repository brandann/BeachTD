using UnityEngine;
using System.Collections;

public class Mapmanager : MonoBehaviour {

	Map currentMap;
	GameObject OpenSpace;
	private float scale = .5f;
	
	private int hits = 0;
	
	GUIText gt;
	
	
	// Use this for initialization
	void Start () {
	
		gt = GameObject.Find("LifeGUIText").GetComponent<GUIText>();
		
		
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
		
		gt.text = "Life: " + (currentMap.getHitsMax() - hits) + "/" + currentMap.getHitsMax();
		
	}
	
	private float spawnbuttoninterval = 0;
	private float spawntimedinterval = 0;
	
	// Update is called once per frame
	void Update () {
	
		
		
	
		// manual input
		if(Input.GetAxis("Fire1") > 0f)
		{
			if ((Time.realtimeSinceStartup - spawnbuttoninterval) > .25f) {
				GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
				GameObject e = Instantiate(enemy) as GameObject;
				spawnbuttoninterval = Time.realtimeSinceStartup;
			}
		}
		
		// automatic input
		if ((Time.realtimeSinceStartup - spawntimedinterval) > 2) {
			GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
			GameObject e = Instantiate(enemy) as GameObject;
			spawntimedinterval = Time.realtimeSinceStartup;
		}
		
		
	}
	
	public void enemyhitgoal()
	{
		hits++;
		gt.text = "Life: " + (currentMap.getHitsMax() - hits) + "/" + currentMap.getHitsMax();
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
		//float x = (0 - cameraWidth) + (i * scale) + (scale/2);
		//float y = (0 - camera.orthographicSize) + (j * scale) + (scale/2);
		
		float x = (i * scale) + (scale/2);
		float y = (j * scale) + (scale/2);
		
		// instantiate and set set position for new OpenSpace
		GameObject e = Instantiate(OpenSpace) as GameObject;
		e.transform.position = new Vector3(x, y, 0);
	}
	
	// return the current maps waypoints for enemys to traverse
	public Vector3[] getWaypoints() {
		return currentMap.getWaypoints();
	}
	
	// return the scale of the game
	public float getScale()
	{
		return scale;
	}
	
	//
	// --------------------------------------------------------------------
	// make hard coded maps
	// --------------------------------------------------------------------
	//
	
	private Vector3[] fixWaypoints(Vector3[] v)
	{
		for(int i = 0; i < v.GetLength(0); i++)
		{
			v[i].x = (v[i].x * scale) + (scale/2);
			v[i].y = (v[i].y * scale) + (scale/2);
		}
		
		return v;
	}
	
	// Mirror the map verically so bool array matches
	// screen map.
	private bool[,] fixMap(bool[,] m)
	{
		for(int i = 0; i < m.GetLength(0); i++)
		{
			for (int j = 0; j < m.GetLength(1); j++)
			{
				bool temp = m[i,j];
				m[i,j] = m[m.GetLength(0) -i -1, j];
				m[i=m.GetLength(0) -i -1,j] = temp;
			}
			
			// if i is >= m.getLength(0) -i -1 then the map has been mirrored to
			// the half point and needs to be returned.
			// if the map continues to mirror it will mirrow back to its
			// origianl layout
			if( (i >= m.GetLength(0) -i -1) )
			{
				return m;
			}
		}
		
		return m; // should never reach this
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
			{	O,	O,	O,	O,	O,	O,	I,	I,	I,	O,	O,	O	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	O,	O,	O	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	O,	O,	O	},
		};
		
		// way points to be passed to enimies for map
		// traversal. can be a different type for simplier
		// reversal for journey back.
		Vector3[] waypoints = new Vector3[]
		{
			new Vector3(0,2,0),
			new Vector3(5,2,0),
			new Vector3(5,4,0),
            new Vector3(11,4,0)
		};
		
		int hitsmax = 10;
		
		// call to mirror map so it matchs the array
		map = fixMap(map);
		
		// call to scale down the map waypoints
		waypoints = fixWaypoints(waypoints);
		
		// create new Map() and set to current map
		currentMap = new Map();
		
		// give currentMap the map and waypoint information to be accessed
		// for future use and enemy support
		currentMap.setMap(map,waypoints, hitsmax);
	}
}
