using UnityEngine;
using System.Collections;

public class MainMenuCreditsButton : SlidingUI {

    public MainMenuContinueButton StartButton;
    public SlidingUI CreditsScript;

    public override void Clicked()
    {
 	    base.Clicked();
        StartButton.Slide();
        CreditsScript.Slide();
    }

}
