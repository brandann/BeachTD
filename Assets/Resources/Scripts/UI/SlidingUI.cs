using UnityEngine;
using System.Collections;

public class SlidingUI : ClickableUI
{

    public Animator ButtonAnimController;
	private int _SlideHash;
    

	public virtual void Start(){
		
        //Trigger between states
		_SlideHash = Animator.StringToHash ("Slide");        

        //public AnimationInfo[] GetCurrentAnimationClipState(int layerIndex);

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
