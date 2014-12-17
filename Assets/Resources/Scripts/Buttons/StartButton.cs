using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
		if(Input.touchCount > 0)
		{
			if(guiTexture.HitTest(Input.GetTouch(0).position))
			{
				// send game object OnTouchDown Message
				// do not require the Object to have the method "OnTouchDown"
				if(Input.GetTouch(0).phase == TouchPhase.Began)
				{
					this.SendMessage("OnTouchDown", Input.GetTouch(0).position, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchUp Message
				// do not require the Object to have the method "OnTouchUp"
				if(Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					this.SendMessage("OnTouchUp", Input.GetTouch(0).position, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchStay Message
				// do not require the Object to have the method "OnTouchStay"
				if(Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					this.SendMessage("OnTouchStay", Input.GetTouch(0).position, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchMove Message
				// do not require the Object to have the method "OnTouchMove"
				if(Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					this.SendMessage("OnTouchMove", Input.GetTouch(0).position, SendMessageOptions.DontRequireReceiver);
				}
				
				// send game object OnTouchExit Message
				// do not require the Object to have the method "OnTouchExit"
				if(Input.GetTouch(0).phase == TouchPhase.Canceled)
				{
					this.SendMessage("OnTouchExit", Input.GetTouch(0).position, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
	}
	
	// Touch Began
	// called when the object is first pressed
	public void OnTouchDown()
	{	
		// Load level manager that has int keys for levels
		//LevelManager lvl = new LevelManager();
		
		this.transform.position = new Vector3(
			this.transform.position.x + .1f,
			this.transform.position.y,
			this.transform.position.z
		);
		
		// Load scene
		//Application.LoadLevel(lvl.getLevel1());
	}
	
	// Touch Ended
	// called when the object is stopped being touched
	public void OnTouchUp()
	{
	
	}
	
	// Touch Stationary
	// called when the object is held
	public void OnTouchStay()
	{
	
	}
	
	// Touch Moved
	// called when the touch is moving on the object
	public void OnTouchMoved()
	{
	
	}
	
	// Touch Canceled
	// called when the touch moved off the object
	public void OnTouchExit()
	{
	
	}
	
}
