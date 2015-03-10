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
			Global myGlobal = GameObject.Find("Global").GetComponent<Global>();
			if(myGlobal != null)
			{
				int nextLevel = myGlobal.LoadedLevel + 1;
				if(nextLevel <= 24)
				{
					myGlobal.LoadMap(nextLevel);
					Application.LoadLevel(Global.Scenes.Game.ToString());
				}
			}
		}
	}
}
