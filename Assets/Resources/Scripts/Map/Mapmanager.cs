using UnityEngine;
using System.Collections;

public class Mapmanager : MonoBehaviour {

	private LevelMap _currentMap;
	private GameObject OpenSpace;
	private EnemySpawner _enemySpawner;
	private float _scale = .5f;
	private float _spawnbuttoninterval = 0;
	private float _spawntimedinterval = 0;

	void Start () {
		OpenSpace = Resources.Load("Prefabs/OpenArea") as GameObject;
		//_enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		//if(_enemySpawner == null)
		//{
		//	throw new UnassignedReferenceException();
		//}
		
		_currentMap = new Map000(_scale);
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
			if ((Time.realtimeSinceStartup - _spawnbuttoninterval) > .25f) {
				//_enemySpawner.SpawnEnemy("Enemy", this.StartingPosition);
				GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
				GameObject e = Instantiate(enemy) as GameObject;
				_spawnbuttoninterval = Time.realtimeSinceStartup;
			}
		}
		
		// automatic input
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > 2) {
			//_enemySpawner.SpawnEnemy("Enemy", this.StartingPosition);
			GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
			GameObject e = Instantiate(enemy) as GameObject;
			_spawntimedinterval = Time.realtimeSinceStartup;
		}
	}
	
	// Make OpenSpace traverses trough currentMap's boolean map to
	// find locations for OpenSpace's
	private void makeOpenSpaces()
	{
		// get the currentMaps map to traverse
		bool [,] map = _currentMap.Map;
		
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
		float x = (i * _scale) + (_scale/2);
		float y = (j * _scale) + (_scale/2);
		
		GameObject e = Instantiate(OpenSpace) as GameObject;
		e.transform.position = new Vector3(x, y, 0);
	}
	
	#region Public Accessors
	
	public void SetCurrentMap(int i)
	{
		string org_map = _currentMap.ToString();
		switch(i)
		{
			case 0:
			_currentMap = new Map000(_scale);
			break;
			case 1:
			_currentMap = new Map001(_scale);
			break;
			default:
			break;
		}
		if(org_map != _currentMap.ToString())
		{
			makeOpenSpaces();
		}
	}
	
	public LevelMap CurrentMap
	{
		get{ return _currentMap; }
	}
	
	// return the current maps waypoints for enemys to traverse
	public Vector3[] WayPoints
	{
		get{ return _currentMap.Waypoints; }
	}
	
	// return the scale of the game
	public float Scale
	{
		get{ return _scale; }
	}
	
	public Vector3 StartingPosition
	{
		get{ 
			Vector3 pos = _currentMap.Waypoints[0]; 
			if(pos == null)
			{
				return Vector3.zero;
			}
				return pos;
			
		}
	}
	
	#endregion
}
