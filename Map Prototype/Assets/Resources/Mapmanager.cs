using UnityEngine;
using System.Collections;

public class Mapmanager : MonoBehaviour {

	Map currentMap;
	GameObject OpenSpace;
	
	// Use this for initialization
	void Start () {
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		makeMap1();
		makeOpenSpaces();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void makeOpenSpaces()
	{
		bool [,] map = currentMap.getMap();
		GameManager gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
		float scale = gameManager.getScale();
		Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i,j])
				{
					makeSpace(j, i, scale, camera);
				}
			}
		}
	}
	
	private void makeSpace(int i, int j, float scale, Camera camera)
	{
		float cameraWidth = camera.orthographicSize * camera.aspect;
		Debug.Log("Camera Width: " + cameraWidth);
		
		float x = (0 - cameraWidth) + (i * scale);
		float y = (0 - camera.orthographicSize) + (j * scale);
		
		GameObject e = Instantiate(OpenSpace) as GameObject;
		e.transform.position = new Vector3(x, y, 0);
		
	}
	
	private void makeMap1()
	{
		bool I = true;
		bool O = false;
		bool[,] map = new bool[,]
		{
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	O,	O,	O,	O,	O,	O,	O,	O	},
			{	I,	I,	I,	I,	I,	O,	I,	I,	I,	I,	I,	I,	I	},
			{	O,	O,	O,	O,	O,	O,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
			{	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I,	I	},
		};
		
		Vector2[] waypoints = new Vector2[]
		{
			new Vector2(5,4),
			new Vector2(5,2),
            new Vector2(2,11)
		};
		
		currentMap = new Map();
		currentMap.setMap(map,waypoints);
	}
}
