﻿using UnityEngine;
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

    public override void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        SaveLoad.Load();
        if(SaveLoad.SavedGame != null)
        {
            if ((SaveLoad.SavedGame.HighestCompletedLevel() + 1) < lvl)
                gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.LogError("Game.CurrentGame == NULL");
        }
        
    }

	public void LoadLevel()
	{
        // DEV
        if (-1 == lvl)
        {
            _global.unlockalllevels();
            Application.LoadLevel(Global.Scenes.Menu.ToString());
            return;
        }

		int level = lvl - 1;
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
