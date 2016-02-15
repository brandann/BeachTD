using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    public GameObject Hide;
    public GameObject Show;

    public void SimpleClicked()
    {
        Show.SetActive(true);
        Hide.SetActive(false);
    }
}
