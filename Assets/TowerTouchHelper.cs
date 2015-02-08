using UnityEngine;
using System.Collections;

public class TowerTouchHelper : MonoBehaviour {

    public Tower ParentTower;


    public void OnTouchDown()
    {
        //Debug.Log("OnTouchDown from helper");
        ParentTower.OnTouchDown();
    }


}
