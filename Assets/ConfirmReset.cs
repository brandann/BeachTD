using UnityEngine;
using System.Collections;

public class ConfirmReset : ClickableUI {

    private Global _global;

    void Awake()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (_global == null)
        {
            Debug.LogError("Can't find global");
            return;
        }


    }


    public override void Clicked()
    {
        base.Clicked();
        _global.ResetGame();

    }

}
