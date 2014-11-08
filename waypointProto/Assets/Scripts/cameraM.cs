using UnityEngine;
using System.Collections;

public class cameraM : MonoBehaviour {

	private float dragSpeed;
	private Vector3 dragCurrent;
	private Vector3 dragPrev;
	private const int CAMMAXHEIGHT = 5;
	private const int CAMMAXWIDTH = 5;
	private Vector3 camStartPos;
	private Vector3 nullVector = Vector3.zero;

	void Start () {
		camStartPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
		dragScreenUnity();
#endif
		dragScreenTouch ();
	

//		#include <time.h>
//		
//		extern "C"
//		{
//			clock_t
//				clock$UNIX2003(void)
//			{
//				return clock();
//			}
//		}


	}



	private void dragScreenUnity()
	{
		dragSpeed = 10;
		if (Input.GetMouseButton(0)) // when the mouse is down
		{
			//when the mouse is lifted, the previous vector is set to null
			// makes sure we dont compare a null value
			if (dragPrev == nullVector)
			{
				dragPrev = Input.mousePosition; //our current pos is set to previous in order to calculate on the next update
				return;
			}
			
			dragCurrent = Input.mousePosition; // we already had a previous so it's okay to calculate 
			
		}
		
		if (!Input.GetMouseButton (0)) // return if the mouse is not being held down, also prevents uneeded caluclations
		{
			dragPrev = nullVector;
			return; 
		}
		
		// get the vector from the previous mouse pos to our new mouse position
		Vector3 pos = Camera.main.ScreenToViewportPoint(dragCurrent - dragPrev);
		Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed,0);
		
		//translate the camera pos based on the vector created from previous and current
		transform.Translate(move, Space.World);

		//clamp the camera to a specific set of bounds
		float x = Mathf.Clamp (Camera.main.transform.position.x, camStartPos.x - CAMMAXWIDTH, camStartPos.x + CAMMAXWIDTH);
		float y = Mathf.Clamp (Camera.main.transform.position.y, camStartPos.y - CAMMAXHEIGHT, camStartPos.y + CAMMAXHEIGHT);                                             
		Camera.main.transform.position = new Vector3 (x, y, Camera.main.transform.position.z);
		
		// make the current vector, the previous one 
		dragPrev = dragCurrent;


	}

	private void dragScreenTouch()
	{
		dragSpeed = .25f;
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * dragSpeed * Time.smoothDeltaTime, -touchDeltaPosition.y * dragSpeed* Time.smoothDeltaTime, 0);
		}

		float x = Mathf.Clamp (Camera.main.transform.position.x, camStartPos.x - CAMMAXWIDTH, camStartPos.x + CAMMAXWIDTH);
		float y = Mathf.Clamp (Camera.main.transform.position.y, camStartPos.y - CAMMAXHEIGHT, camStartPos.y + CAMMAXHEIGHT);                                             
		Camera.main.transform.position = new Vector3 (x, y, Camera.main.transform.position.z);
			
	}
}
