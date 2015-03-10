using UnityEngine;
using System.Collections;

public class ResumeButton : ClickableUI {

    void Start() 
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (_global == null)
            Debug.LogError("Can't find global");
    }

    public override void Clicked()
    {
        base.Clicked();
        _global.ResumeGame();
    }

    private Global _global;

}
