using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuLevelButton : ClickableUI {

    public delegate void LevelClicked();
    public static event LevelClicked OnLevelClicked;

    void Start()
    {
        Text text = gameObject.GetComponentInChildren<Text>();
        if (text == null)
            Debug.LogWarning("Missing text UI element");
    }

    public override void Clicked()
    {
        base.Clicked();
        
        if (OnLevelClicked != null)
            OnLevelClicked();
    }

	

}
