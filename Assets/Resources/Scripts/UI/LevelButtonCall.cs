using UnityEngine;
using System.Collections;

public class LevelButtonCall : MonoBehaviour {


	public void LoadLevel(int i)
	{
		Debug.Log ("Load Level: " + i);
		GameObject.Find("Global").GetComponent<Global>().LoadMap(i);
		Application.LoadLevel(Global.Scenes.Game.ToString()); 
	}
	
}
