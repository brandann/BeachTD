using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private float SnapGrid = .5f;
	private Vector3 BadVector = new Vector3(6,6,6);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void createTower(Vector3 pos)
	{
		pos.z = 0f;
		GameObject towerPrefab = Resources.Load("Prefabs/ShockTowerBlue") as GameObject;
			
		Debug.Log("Create Tower");
		
		Vector3 newPos = getPos(pos);
		if(newPos != BadVector)
		{
			GameObject e = Instantiate(towerPrefab) as GameObject;
			TowerBehavior spawnedTower = e.GetComponent<TowerBehavior>();
			if(spawnedTower != null) {
				e.transform.position = newPos;
			}
		}
			
	}
	
	// snap tower position to .5 unit grid
	private Vector3 getPos(Vector3 v)
	{
		Vector3 p = new Vector3(v.x, v.y, 0f);
		
		// round x to nearest snap point
		if((v.x % SnapGrid) < (SnapGrid/2)){ // round x down
			p.x -= (v.x % SnapGrid);
		}
		else if((v.x % SnapGrid) < SnapGrid){ // round x up
			p.x += (SnapGrid - (v.x % SnapGrid));
		}
		
		// round y to nearest snap point
		if((v.y % SnapGrid) < (SnapGrid/2)){ // round y down
			p.y -= (v.y % SnapGrid);
		}
		else if((v.y % SnapGrid) < SnapGrid){ // round y up
			p.y += (SnapGrid - (v.y % SnapGrid));
		}
		
		Vector2 p2 = new Vector2(p.x, p.y);
		
		RaycastHit2D hit = Physics2D.Raycast(p, Vector2.zero);
		if(hit.collider == null)
			Debug.Log("no colliders");
		else if(hit != null && hit.collider.tag == "tower")
		{
			Debug.Log("Found Tower where building");
			return BadVector;
		}
		
		return p;
	}
}
