using UnityEngine;
using System.Collections;

public class ReplayLevelButton : ClickableUI {	

    public override void Clicked()
    {
        base.Clicked();
        Application.LoadLevel(Global.Scenes.Game.ToString());
    }
	
	
}
