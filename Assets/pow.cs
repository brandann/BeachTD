using UnityEngine;
using System.Collections;

public class pow : MonoBehaviour {

	float SpawnTime = 1;
	float _spawntimedinterval;

	// Use this for initialization
	void Start () {
		_spawntimedinterval = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.realtimeSinceStartup - _spawntimedinterval) > SpawnTime) 
		{
			Destroy(this.gameObject);
		}
	}
}
