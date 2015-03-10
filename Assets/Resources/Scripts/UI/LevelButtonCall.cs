using UnityEngine;
using System.Collections;

public class LevelButtonCall : MonoBehaviour {


	public void LoadLevel(int i)
	{
		int level = i - 1;
		Debug.Log ("Load Level: " + level);
		GameObject.Find("Global").GetComponent<Global>().LoadMap(level);
		Application.LoadLevel(Global.Scenes.Game.ToString()); 
	}
	
}
