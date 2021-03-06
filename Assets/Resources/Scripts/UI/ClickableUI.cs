﻿using UnityEngine;
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

    //Didn't want to refactor things the right way
    protected void OtherClicked()
    {
        if (OnButtonClicked != null)
            OnButtonClicked();
    }
    
    protected void turnOffAllTowers()
    {
    	var gos = GameObject.FindGameObjectsWithTag("tower");
    	for(int i = 0; i < gos.Length; i++){
    		gos[i].SetActive(false);
    	}
    }
}
