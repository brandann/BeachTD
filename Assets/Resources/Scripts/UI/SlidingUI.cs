using UnityEngine;
using System.Collections;

public class SlidingUI : ClickableUI
{

    protected Animator ButtonAnimController;
	protected int _SlideHash;

    protected virtual void Awake()
    {
        ButtonAnimController = gameObject.GetComponent<Animator>();
        if (ButtonAnimController == null){
            Debug.LogWarning("This class is designed to trigger UI Animations using Mecanim this object doesn't have an animator");
        }
    }


	public virtual void Start(){
		
        //Trigger between states
		_SlideHash = Animator.StringToHash ("Slide");        

        
        /*
        //Speed up animation in debug mode
        if (Debug.isDebugBuild)
        {
            ButtonAnimController.speed = 5;
        }
        else
        {
            Animator.StringToHash("SlideInRight");
        }
        */
        
	}
	
	public void Slide(){
        if (null != ButtonAnimController)
        {
            ButtonAnimController.SetTrigger("Slide");
        }
	}

    public override void Clicked()
    {
        base.Clicked();
        Slide();
    }
}
