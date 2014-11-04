using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

  // private refence to GameManager
	GameManager gameManager;
	
	// messages for handeling mouse conversion to touch
	private string message = "";
	private const string touchDown = "OnTouchDown";
	private const string touchUp   = "OnTouchUp";
	private const string touchStay = "OnTouchStay";
	private const string touchMove = "OnTouchMove";
	private const string touchExit = "OnTouchExit";

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
		//Input.simulateMouseWithTouches = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		// location vectors
		Vector2 touchPos = Vector2.zero;
		Vector3 wp = Vector3.zero;
		Vector3 dp = Vector3.zero;
		string message = "";
		
		
#region touch/mouse
#if UNITY_EDITOR
		// use mouse for Unity Editor
    // turns mouse button actions into faux touch
    // inputs only in Unity Editor
    // can add for computer for PC/Mac port
		Debug.Log("TouchController In Use");
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			// set dp to the mouse position
			dp = Input.mousePosition;
			
			// send game object OnTouchUp Message
			// do not require the Object to have the method "OnTouchUp"
			if(Input.GetMouseButtonUp(0))
			{
				message = touchUp;
			}
			
			// send game object OnTouchStay Message
			// do not require the Object to have the method "OnTouchStay"
			if(Input.GetMouseButton(0))
			{
				message = touchStay;
			}
			
			// send game object OnTouchDown Message
			// do not require the Object to have the method "OnTouchDown"
			if(Input.GetMouseButtonDown(0))
			{
				message = touchDown;
			}
		}
#endif
		
    // Touch controller
    // find the first touch instance, then find what kind of
    // touch happened.
		if(Input.touchCount > 0)
		{
			// get the first touch
			Touch touch = Input.GetTouch(0);
			
			// log the touch position
			dp = touch.position;
			
			// send game object OnTouchDown Message
			// do not require the Object to have the method "OnTouchDown"
			if(touch.phase == TouchPhase.Began)
			{
				message = touchDown;
			}
			
			// send game object OnTouchUp Message
			// do not require the Object to have the method "OnTouchUp"
			if(touch.phase == TouchPhase.Ended)
			{
				message = touchUp;
			}
			
			// send game object OnTouchStay Message
			// do not require the Object to have the method "OnTouchStay"
			if(touch.phase == TouchPhase.Stationary)
			{
				message = touchStay;
			}
			
			// send game object OnTouchMove Message
			// do not require the Object to have the method "OnTouchMove"
			if(touch.phase == TouchPhase.Moved)
			{
				message = touchMove;
			}
			
			// send game object OnTouchExit Message
			// do not require the Object to have the method "OnTouchExit"
			if(touch.phase == TouchPhase.Canceled)
			{
				message = touchExit;
			}
			
		}
#endregion
		
		// if message is blank, then assume nothing has happened
		// if selection is found, try to do something
		if(message != "")
		{
			// set world point
			// set vector2 world point
			wp = Camera.main.ScreenToWorldPoint(dp);
			touchPos = new Vector2(wp.x, wp.y);
			
			// If colliders are found at touchPos point
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(dp), Vector2.zero);
			
			if(message == touchDown)
			{
				// if tower tag is found
				if(hit.transform.tag == "tower")
				{
					Debug.Log("Found Tower");
					hit.transform.gameObject.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				
				// if tower tag is found
				else if(hit.transform.tag == "open")
				{
					Debug.Log("Found Open Space");
					hit.transform.gameObject.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}
	}
}
