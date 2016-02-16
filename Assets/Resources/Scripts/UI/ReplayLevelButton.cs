using UnityEngine;
using System.Collections;

public class ReplayLevelButton : ClickableUI {	

    public override void Clicked()
    {
        base.Clicked();
		turnOffAllTowers();
        Application.LoadLevel(Global.Scenes.Game.ToString());
    }
	
	
}
