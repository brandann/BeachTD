﻿using UnityEngine;
using System.Collections;

public class SlidingUI : ClickableUI
{

    private Animator ButtonAnimController;
	private int _SlideHash;

    void Awake()
    {
        ButtonAnimController = gameObject.GetComponent<Animator>();
        if (ButtonAnimController == null)
            Debug.LogError("missing animator");
    }


	public virtual void Start(){
		
        //Trigger between states
		_SlideHash = Animator.StringToHash ("Slide");        

        

        //Speed up animation in debug mode
        if (Debug.isDebugBuild)
        {
            ButtonAnimController.speed = 5;
        }
        else
        {
            Animator.StringToHash("SlideInRight");
        }
        
	}
	
	public void Slide(){
		ButtonAnimController.SetTrigger (_SlideHash);
	}

    public override void Clicked()
    {
        base.Clicked();
        Slide();
    }
}
