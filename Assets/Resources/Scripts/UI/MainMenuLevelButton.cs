using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuLevelButton : ClickableUI {

    public delegate void LevelClicked();
    public static event LevelClicked OnLevelClicked;
    protected Animator _anim;

    void Start()
    {
        Text text = gameObject.GetComponentInChildren<Text>();
        if (text == null)
            Debug.LogWarning("Missing text UI element");

        _anim = gameObject.GetComponent<Animator>();
    }

    public override void Clicked()
    {
        base.Clicked();
        
        if (OnLevelClicked != null)
            OnLevelClicked();
    }

    public void Slide()
    {
        _anim.SetTrigger("Slide");
    }

	

}
