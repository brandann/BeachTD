using UnityEngine;

using System.Collections;

public class DoNotReset : ClickableUI {

    //set in Inspector
    public GameObject ResetPanel;

    public override void Clicked()
    {
        base.Clicked();
        ResetPanel.SetActive(false);
    }

    

}
