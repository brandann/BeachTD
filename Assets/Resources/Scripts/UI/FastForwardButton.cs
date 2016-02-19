using UnityEngine;
using System.Collections;

public class FastForwardButton : MonoBehaviour {

    private bool fast;

    void Start() { fast = false; }

	public void FastForwardClicked()
    {
        if(!fast)
        {
            Time.timeScale = 3;
            this.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            Time.timeScale = 1;
            this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        fast = !fast;
    }

}
