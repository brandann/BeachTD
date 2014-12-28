using UnityEngine;
using System.Collections;

public class MainMenuStartButton : SlidingUI{

	public MainMenuCreditsButton Credits;
    public AnimationClip SlideAnimation;
    
    private Camera mSceneCam;
    private float mSlideTime;
    

    public override void Start()
    {
        base.Start();
        mSceneCam = Camera.main;
        mSlideTime = SlideAnimation.length;
    }

	public override void Clicked ()
	{
		base.Clicked ();
		Credits.Slide ();
        StartCoroutine(LoadLevel());
	}

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(mSlideTime);
        Application.LoadLevel(1);
     
    }
	
}
