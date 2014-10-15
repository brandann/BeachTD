using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void createTower(Vector3 pos, int type)
	{
		pos.z = 0f;
		GameObject towerPrefab = null;
		if(type == 0)
			towerPrefab = Resources.Load("Prefabs/ShockTowerYellow") as GameObject;
		else if (type == 1)
			towerPrefab = Resources.Load("Prefabs/GeneratorBlue") as GameObject;
		else if (type == 2)
			towerPrefab = Resources.Load("Prefabs/GeneratorRogue") as GameObject;
		else if (type == 3)
			towerPrefab = Resources.Load("Prefabs/ShockTowerBlue") as GameObject;
			
		Debug.Log("Create Tower");
		
		GameObject e = Instantiate(towerPrefab) as GameObject;
		TowerBehavior spawnedTower = e.GetComponent<TowerBehavior>();
		if(spawnedTower != null) {
			Vector3 newPos = getPos(pos);
			e.transform.position = newPos;
		}
	}
	
	private Vector3 getPos(Vector3 v)
	{
		if(v.x % .5 != 0)
		{
			// x is not a multiple of .5
			Debug.Log("x not aligned");
			
		}
		
		if(v.y % .5 != 0)
		{
			// y is not a multiple of .5
			Debug.Log("y not aligned");
			
		}
	}
}
