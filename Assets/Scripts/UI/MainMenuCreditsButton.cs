using UnityEngine;
using System.Collections;

public class MainMenuCreditsButton : SlidingUI {

    public MainMenuStartButton StartButton;
    public SlidingUI CreditsScript;

    public override void Clicked()
    {
 	    base.Clicked();
        StartButton.Slide();
        CreditsScript.Slide();
    }

}
