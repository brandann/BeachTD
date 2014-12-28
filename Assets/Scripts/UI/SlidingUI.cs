using UnityEngine;
using System.Collections;

public class SlidingUI : MonoBehaviour {

	public Animator ButtonAnimController;
	private int mSlideHash;
    private int mSlideInState;

	public virtual void Start(){
		
        //Trigger between states
		mSlideHash = Animator.StringToHash ("Slide");
        
        //State with button on screen 
        mSlideInState = Animator.StringToHash("SlideInRight");
	}
	
	public void Slide(){
		ButtonAnimController.SetTrigger (mSlideHash);
	}

	public virtual void Clicked(){

		Slide ();
	}
}
