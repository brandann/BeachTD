using UnityEngine;
using System.Collections;

public class NextLevel : ClickableUI {

    private Global _global;

    void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
    }
    


    public override void Clicked()
    {
        base.Clicked();
        _global.LoadMap(_global.LoadedLevel + 1);
        Application.LoadLevel(Global.Scenes.Game.ToString());
    }
}
