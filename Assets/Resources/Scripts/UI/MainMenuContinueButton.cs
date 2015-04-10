using UnityEngine;
using System.Collections;

public class MainMenuContinueButton : SlidingUI{

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
        //SaveLoad s = new SaveLoad();    
        
	}

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(_slideTime);
		//Application.LoadLevel(Global.Scenes.Levels.ToString());     
    }
	
}
