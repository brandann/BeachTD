using UnityEngine;
using System.Collections;

public class MainMenuLevelButton : SlidingUI {

    public delegate void LevelClicked();
    public static event LevelClicked OnLevelClicked;

    public override void Clicked()
    {
        base.Clicked();
        
        if (OnLevelClicked != null)
            OnLevelClicked();
    }
	

}
