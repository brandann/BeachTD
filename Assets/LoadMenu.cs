using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {

	private float _spawntimedinterval;
	
	// Use this for initialization
	void Start () {
		_spawntimedinterval = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > 3)
		{
			Application.LoadLevel("Menu");
		}
	}
}
