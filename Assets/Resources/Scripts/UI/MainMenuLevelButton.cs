using UnityEngine;
using System.Collections;

public class MainMenuLevelButton : ClickableUI {

    public override void Clicked()
    {
        base.Clicked();
        Application.LoadLevel(Global.Scenes.Levels.ToString());
    }
	

}
