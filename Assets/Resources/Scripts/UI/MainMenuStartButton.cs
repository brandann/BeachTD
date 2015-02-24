﻿using UnityEngine;
using System.Collections;

public class MainMenuStartButton : SlidingUI{

	public MainMenuCreditsButton Credits;
    public AnimationClip SlideAnimation;    
    
    private float _slideTime;    

    public override void Start()
    {
        base.Start();        
        _slideTime = SlideAnimation.length;
    }

	public override void Clicked ()
	{
		base.Clicked ();
		//Credits.Slide ();
        //StartCoroutine(LoadLevel()); //Todo swap these out for release
        Application.LoadLevel(Global.Scenes.Game.ToString()); 
	}

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(_slideTime);
		//Application.LoadLevel(Global.Scenes.Levels.ToString());     
    }
	
}
