using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	
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

    private void HandlePause()
    {
        gameObject.SetActive(true);
    }

    private void HandleResume()
    {
        gameObject.SetActive(false);
    }
	
	
}
