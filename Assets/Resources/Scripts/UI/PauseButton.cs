using UnityEngine;
using System.Collections;

public class PauseButton : ClickableUI {

    

    void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (_global == null)
            Debug.LogError("Can't find _global");

        gameObject.SetActive(true); 
    }

    private void HandlePause()
    {
        gameObject.SetActive(false);            
    }

    private void HandleResume()
    {       
        gameObject.SetActive(true);    
    }

    void OnEnable()
    {
        Global.OnGamePaused += HandlePause;
        Global.OnGameResumed += HandleResume;
    }

    void OnDestroy()
    {
        Global.OnGamePaused -= HandlePause;
        Global.OnGameResumed -= HandleResume;

    }

    public override void Clicked()
    {
        base.Clicked();
        _global.PauseGame();        
    }

    private Global _global;
	
}
