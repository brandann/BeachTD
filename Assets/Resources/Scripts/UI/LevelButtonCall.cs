using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelButtonCall : MonoBehaviour {
    
    //Set in inspector
    public int lvl;

    void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (Game.CurrentGame.CurrentLevel < lvl)
            gameObject.GetComponent<Button>().interactable = false;
    }

	public void LoadLevel()
	{
		int level = lvl - 1;
		Debug.Log ("Load Level: " + level);
		_global.LoadMap(level);
		Application.LoadLevel(Global.Scenes.Game.ToString()); 
	}

    private Global _global;
	
}
