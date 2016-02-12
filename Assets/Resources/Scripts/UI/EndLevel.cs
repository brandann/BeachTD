using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevel : MonoBehaviour {
    public Button NextLevel;
    public Button Replay;
    public Button MainMenu;    

    void Start()
    {
        Global.OnGameWon += HandleWin;
        Global.OnGameLost += HandleLose;

       
        _global = GameObject.Find("Global").GetComponent<Global>();
        _message = gameObject.GetComponentInChildren<Text>();
        if (_message == null)
            Debug.LogWarning("Can't find text");
        gameObject.SetActive(false);
    }

    private Global _global;
    private Text _message;

    private void HandleWin()
    {
        gameObject.SetActive(true);
        NextLevel.gameObject.SetActive(_global.LoadedLevel <= Global.MaxLevels);
        _message.text = "Congratulations You Win!";
    }

    private void HandleLose()
    {
        gameObject.SetActive(true);
        NextLevel.gameObject.SetActive(false);
        _message.text = "Game Over!";
    }

    void OnDestroy()
    {
        Global.OnGameWon -= HandleWin;
        Global.OnGameLost -= HandleLose;
    }

}
