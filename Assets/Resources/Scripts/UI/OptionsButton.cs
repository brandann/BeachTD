using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class OptionsButton : MonoBehaviour {

    public GameObject Hide;
    public GameObject Show;

    public void SimpleClicked()
    {
        Show.SetActive(true);
        Hide.SetActive(false);
    }

    public void ShowAdSimple()
    {
        if (Advertisement.IsReady())
        {
            print("Ads should be showing - Brandan");
            Advertisement.Show();
        }
        SimpleClicked();
    }
}
