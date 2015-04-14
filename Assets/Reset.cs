using UnityEngine;
using System.Collections;

public class Reset : SlidingUI {

    public GameObject ResetPanel;

    override protected void Awake()
    {
        base.Awake();
        MainMenuLevelButton.OnLevelClicked += HandleLevelSelection;

    }

    void OnDestroy()
    {
        MainMenuLevelButton.OnLevelClicked -= HandleLevelSelection;
    }


    public override void Clicked()
    {
        base.Clicked();
        ResetPanel.SetActive(true);

    }

    private void HandleLevelSelection()
    {
        Slide();
    }



}
