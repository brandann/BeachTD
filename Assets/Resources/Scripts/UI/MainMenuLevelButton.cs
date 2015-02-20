using UnityEngine;
using System.Collections;

public class MainMenuLevelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Clicked()
	{
		Application.LoadLevel(Global.Scenes.Levels.ToString());
	}
}
