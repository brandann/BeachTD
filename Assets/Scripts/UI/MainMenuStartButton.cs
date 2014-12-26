using UnityEngine;
using System.Collections;

public class MainMenuStartButton : SlidingButton{

	public override void Clicked ()
	{
		base.Clicked ();
	}

	void OnBecameInvisible(){
		Application.LoadLevel (1);
	}
	
}
