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
			int nextLevel = myGlobal.LoadedLevel;
			
			if(Application.loadedLevelName == Global.Scenes.Win.ToString())
			{
				nextLevel += 1;
			}
			else if (Application.loadedLevelName == Global.Scenes.Lose.ToString())
			{
				//nothing
			}
			
			if(myGlobal != null)
			{
				if(nextLevel <= 24)
				{
					myGlobal.LoadMap(nextLevel);
					Application.LoadLevel(Global.Scenes.Game.ToString());
				}
			}
		}
	}
}
