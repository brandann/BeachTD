using UnityEngine;
using System.Collections;

public class PauseButton : ClickableUI {

    

    void Start()
    {
        _global = GameObject.Find("Global").GetComponent<Global>();
        if (_global == null)
            Debug.LogError("Can't find _global");

        gameObject.SetActive(true);

        Global.OnGamePaused += HandlePause;
        Global.OnGameResumed += HandleResume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            gameObject.SetActive(false);
            Clicked();
        }
    }

    private void HandlePause()
    {
        if (null != gameObject)
        {
            gameObject.SetActive(false);
        }
    }

    private void HandleResume()
    {       
        gameObject.SetActive(true);    
    }

    void OnEnable()
    {
        
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
