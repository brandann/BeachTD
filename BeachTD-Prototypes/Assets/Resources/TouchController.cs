using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	
		if(Input.touchCount > 0 || Input.GetMouseButtonUp(0))
		{
			// location vectors
			Vector2 touchPos = Vector2.zero;
			Vector3 wp = Vector3.zero;
			Vector3 dp = Vector3.zero;
			
			// find touch or click
			if(Input.touchCount > 0)
			{
				//Debug.Log("Touch Found");
				dp = Input.GetTouch(0).position;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				//Debug.Log("Mouse Click Found");
				dp = Input.mousePosition;
			}
			
			wp = Camera.main.ScreenToWorldPoint(dp);
			
			// if selection is found, try to destroy object
			if(wp != Vector3.zero)
			{
				touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapPoint(touchPos))
				{
					Debug.Log("Make Tower");
					gameManager.createTower(wp, Random.Range(0,3));
				}
				else
				{
					RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(dp), Vector2.zero);
					if(hit.collider.tag == "tower")
					{
						Debug.Log("Found Tower to Destroy");
						TowerBehavior t = hit.collider.gameObject.GetComponent<TowerBehavior>();
						if(t != null)
						{
							t.destroy();
						}
					}
				}
			}
		}
	}
	

}
