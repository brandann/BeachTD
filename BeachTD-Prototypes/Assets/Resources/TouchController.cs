using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
		if(Input.touchCount > 0 || Input.GetMouseButtonUp(0))
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
				touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapPoint(touchPos))
				{
					Debug.Log("Make Tower");
				}
				else
				{
					Debug.Log("Destroy Tower");
				}
			}
		}
	}
}
