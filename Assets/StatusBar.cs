using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusBar : MonoBehaviour {

    public Text DollarDisplay;
    public Text EggDisplay;    

    void Start()
    {
        if(DollarDisplay == null || EggDisplay == null)
            Debug.LogError("Missing UI elements");
    }

    public void SetDollars(int dollars)
    {
        DollarDisplay.text = dollars.ToString();
    }

    public void SetEggs(int eggs)
    {
        EggDisplay.text = eggs.ToString();
    }



}
