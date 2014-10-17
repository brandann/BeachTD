using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	private GUITexture myGuiTextObject;
	
	// Use this for initialization
	void Start () {
		myGuiTextObject = GameObject.Find("StartButton") as GUITexture;
	}
	
	// Update is called once per frame
	void Update () {
	
		Touch touchObj = Input.GetTouch(0);
		
		if (touchObj.phase == Input.Began)
		{
			if(myGuiTextObject.HitTest(touchObj.position))
			{
				Debug.Log ("Startbutton");
			}
		}
		
	}
}
