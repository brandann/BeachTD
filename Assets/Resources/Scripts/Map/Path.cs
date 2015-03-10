using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
    public delegate void PathTouched();
    public static event PathTouched OnPathTouched;

    public void OnTouchDown()
    {
        if (OnPathTouched != null)
            OnPathTouched();
    }

}
