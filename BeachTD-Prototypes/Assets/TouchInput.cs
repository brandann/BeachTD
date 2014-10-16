using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask touchInputMask;
	
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	
	private RaycastHit hit;
	
	public bool use;
	
	// Update is called once per frame
	void Update () {
	
		if (!use)
		{
			return;
		}
		
		Debug.Log("TouchController In Use");
	
#if UNITY_EDITOR

		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			// make Ray at touch location
			Ray uray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			// if ray hits Game Object
			if(Physics.Raycast(uray, out hit, touchInputMask))
			{
				// Gameobject found
				GameObject rec = hit.transform.gameObject;
				
				// send game object OnTouchDown Message
				// do not require the Object to have the method "OnTouchDown"
				if(Input.GetMouseButtonDown(0))
				{
					rec.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchUp Message
				// do not require the Object to have the method "OnTouchUp"
				if(Input.GetMouseButtonUp(0))
				{
					rec.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchStay Message
				// do not require the Object to have the method "OnTouchStay"
				if(Input.GetMouseButton(0))
				{
					rec.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
			// send a message to all objects that were tocuhed, but are no longer touched due to sliding finger
			foreach(GameObject g in touchesOld)
			{
				if(!touchList.Contains(g))
				{
					// send game object OnTouchExit Message
					// do not require the Object to have the method "OnTouchExit"
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		
#endif

		if(Input.touchCount > 0)
		{
			// make a list of the last GameObjects touched
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();
			
			// cycle through all the touch inputs in queue
			foreach (Touch touch in Input.touches) 
			{
				// make Ray at touch location
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				
				// if ray hits Game Object
				if(Physics.Raycast(ray, out hit, touchInputMask))
				{
					// Gameobject found
					GameObject rec = hit.transform.gameObject;
					
					// send game object OnTouchDown Message
					// do not require the Object to have the method "OnTouchDown"
					if(touch.phase == TouchPhase.Began)
					{
						rec.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					// send game object OnTouchUp Message
					// do not require the Object to have the method "OnTouchUp"
					if(touch.phase == TouchPhase.Ended)
					{
						rec.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					// send game object OnTouchStay Message
					// do not require the Object to have the method "OnTouchStay"
					if(touch.phase == TouchPhase.Stationary)
					{
						rec.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					// send game object OnTouchMove Message
					// do not require the Object to have the method "OnTouchMove"
					if(touch.phase == TouchPhase.Moved)
					{
						rec.SendMessage("OnTouchMove", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					// send game object OnTouchExit Message
					// do not require the Object to have the method "OnTouchExit"
					if(touch.phase == TouchPhase.Canceled)
					{
						rec.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			
			// send a message to all objects that were tocuhed, but are no longer touched due to sliding finger
			foreach(GameObject g in touchesOld)
			{
				if(!touchList.Contains(g))
				{
					// send game object OnTouchExit Message
					// do not require the Object to have the method "OnTouchExit"
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}

}
