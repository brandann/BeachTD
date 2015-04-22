using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelButtonCall : SlidingUI {
    
    //Set in inspector
    public int lvl;

    protected override void Awake()
    {
        base.Awake();        
        MainMenuLevelButton.OnLevelClicked += HandleLevelSelection;
    }

    void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        SaveLoad.Load();
        Game.CurrentGame = SaveLoad.SavedGame;
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

    protected virtual void HandleLevelSelection()
    {
        base.Slide();
        //Animator ani = gameObject.GetComponent<Animator>();
        //ani.SetTrigger("Slide");
    }

    void OnDestroy()
    {
        MainMenuLevelButton.OnLevelClicked -= HandleLevelSelection;

    }

    private Global _global;
	
}
