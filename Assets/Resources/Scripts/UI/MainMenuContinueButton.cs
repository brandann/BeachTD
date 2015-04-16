using UnityEngine;
using System.Collections;

public class MainMenuContinueButton : SlidingUI{

	public MainMenuCreditsButton Credits;
    public AnimationClip SlideAnimation;    
    
    private float _slideTime;
    private Global _global;

    public override void Start()
    {
        base.Start();        
        _slideTime = SlideAnimation.length;
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (_global == null)
            Debug.LogError("missing global");

    }

	public override void Clicked ()
	{
		base.Clicked ();
        _global.Continue();
		 
        
	}

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(_slideTime);
		//Application.LoadLevel(Global.Scenes.Levels.ToString());     
    }
	
}
