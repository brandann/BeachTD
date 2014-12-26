using UnityEngine;
using System.Collections;

public class MainMenuStartButton : SlidingButton{

	public MainMenuCreditsButton Credits;

	public override void Clicked ()
	{
		base.Clicked ();
		Credits.Slide ();
	}

	void OnBecameInvisible(){
		Application.LoadLevel (1);
	}
	
}
