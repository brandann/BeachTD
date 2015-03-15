using UnityEngine;
using System.Collections;

public class MainMenuButton : ClickableUI {

    public override void Clicked()
    {
        base.Clicked();
        Application.LoadLevel(Global.Scenes.Menu.ToString());
    }
}
