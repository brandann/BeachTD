using UnityEngine;
using System.Collections;

public class Mapmanager : MonoBehaviour {

	private LevelMap currentMap;
	private GameObject OpenSpace;
	private float scale = .5f;
	private float spawnbuttoninterval = 0;
	private float spawntimedinterval = 0;

	void Start () {
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		
		currentMap = new Map000(scale);
		//currentMap = new Map001(scale);
		
		// build the OpenSapces for Map1
		// this is for testing and should be
		// removed later when we dont want a map created right away!
		makeOpenSpaces();
	}
	
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
	
	// Make OpenSpace traverses trough currentMap's boolean map to
	// find locations for OpenSpace's
	private void makeOpenSpaces()
	{
		// get the currentMaps map to traverse
		bool [,] map = currentMap.Map;
		
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
		float cameraWidth = camera.orthographicSize * camera.aspect;
		float x = (i * scale) + (scale/2);
		float y = (j * scale) + (scale/2);
		
		GameObject e = Instantiate(OpenSpace) as GameObject;
		e.transform.position = new Vector3(x, y, 0);
	}
	
	// return the current maps waypoints for enemys to traverse
	public Vector3[] WayPoints
	{
		get{ return currentMap.Waypoints; }
	}
	
	// return the scale of the game
	public float Scale
	{
		get{ return currentMap.Scale; }
	}
}
