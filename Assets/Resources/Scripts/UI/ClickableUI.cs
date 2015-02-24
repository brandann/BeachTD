using UnityEngine;
using System.Collections;

public class ClickableUI : MonoBehaviour {

    #region Events
    public delegate void ButtonClicked();
    public static event ButtonClicked OnButtonClicked;

    #endregion 

    public virtual void Clicked()
    { 
        if (OnButtonClicked != null)
            OnButtonClicked();
    }


}
