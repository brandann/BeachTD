using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        Global.OnGamePaused += HandlePause;
        Global.OnGameResumed += HandleResume;
	}

    void OnEnable()
    {
        
    }

    void OnDestroy()
    {
        Global.OnGamePaused -= HandlePause;
        Global.OnGameResumed -= HandleResume;
    }

    private void HandlePause()
    {
        gameObject.SetActive(true);
    }

    private void HandleResume()
    {
        gameObject.SetActive(false);
    }
	
	
}
