using UnityEngine;
using System.Collections;

public class Reset : SlidingUI {

    public GameObject ResetPanel;

    protected override void Awake()
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
        OtherClicked();

        ResetPanel.SetActive(true);

        //Slide();

        //Animator _anim = gameObject.GetComponent<Animator>();
        //_anim.SetTrigger("Slide");
        
    }

    private void HandleLevelSelection()
    {
        Slide();
    }



}
