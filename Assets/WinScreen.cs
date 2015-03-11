using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinScreen : MonoBehaviour {
    public Button NextLevel;
    public Button Replay;
    public Button MainMenu;
    

    void Start()
    {
        Global.OnGameWon += HandleWin;
        gameObject.SetActive(false);
        _global = GameObject.Find("Global").GetComponent<Global>();

    }

    private Global _global;

    private void HandleWin()
    {
        gameObject.SetActive(true);
        NextLevel.gameObject.SetActive(_global.LoadedLevel <= Global.MaxLevels);
    }

    void OnDestroy()
    {
        Global.OnGameWon -= HandleWin;
    }

}
