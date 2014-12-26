using UnityEngine;
using System.Collections;

public class SlidingButton : MonoBehaviour {

	public Animator ButtonAnimController;
	private int mSlideHash;
	
	void Start(){
		
		mSlideHash = Animator.StringToHash ("Slide");
	}
	
	public void Slide(){
		ButtonAnimController.SetTrigger (mSlideHash);
	}

	public virtual void Clicked(){
		//Ignore multiple clicks while sliding out
		if (ButtonAnimController.animation.isPlaying)
						return;
					
		Slide ();
	}
}
