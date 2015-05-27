using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusBar : MonoBehaviour {

    public Text DollarDisplay;
    public Text EggDisplay;
    public Text GameOverEggDisplay;

    void OnEnable()
    {
        EggManager.OnEggCountChanged += SetEggs;
        SandDollarBank.OnSandDollarsChanged += SetDollars;
    }

    void OnDisable()
    {
        EggManager.OnEggCountChanged -= SetEggs;
        SandDollarBank.OnSandDollarsChanged -= SetDollars;
    }

    void Start()
    {
        if(DollarDisplay == null || EggDisplay == null)
            Debug.LogError("Missing UI elements");
    }

    private void SetDollars(int dollars)
    {
        DollarDisplay.text = dollars.ToString();
    }

    public void SetEggs(int eggs)
    {
        EggDisplay.text = eggs.ToString();
        GameOverEggDisplay.text = eggs.ToString();
    }



}
