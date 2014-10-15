using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
		{
			Vector3 touchPosition = Input.GetTouch(0).position;
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);
			RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
			if(hit.collider != null)
			{
				if(hit.collider.gameObject.tag == "tower")
				{
					Destroy(hit.collider.gameObject);
				}
				else
				{
					createTower(touchPosition);
				}
			}
			
		}
		//select ();
	}
	
	private void createTower(Vector3 pos)
	{
		Debug.Log("Create Tower");
		GameObject towerPrefab = Resources.Load("Prefabs/ShockTowerYellow") as GameObject;
		GameObject e = Instantiate(towerPrefab) as GameObject;
		TowerBehavior spawnedTower = e.GetComponent<TowerBehavior>();
		if(spawnedTower != null) {
			e.transform.position = pos;
		}
	}
	
	private void select()
	{
		// location vectors
		Vector2 touchPos = Vector2.zero;
		Vector3 wp = Vector3.zero;
		
		// find touch or click
		if(Input.touchCount > 0)
		{
			//Debug.Log("Touch Found");
			wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			//Debug.Log("Mouse Click Found");
			wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		
		// if selection is found, try to destroy object
		if(wp != Vector3.zero)
		{
			//RaycastHit2D hit = RaycastHit2D
			touchPos = new Vector3(wp.x, wp.y, 0f);
				
			
		}
		else
		{
			Input.simulateMouseWithTouches = true;
		}
	}
}
