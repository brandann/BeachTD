using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

	private float delay;
	private float _spawntimedinterval;
	
	// Use this for initialization
	void Start () {
		delay = 1.5f;
		_spawntimedinterval = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > delay)
		{
			Application.LoadLevel(Global.Scenes.Menu.ToString());
		}
	}
}
