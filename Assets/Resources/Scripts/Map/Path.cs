using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
    public delegate void PathTouched(Path path);
    public static event PathTouched OnPathTouched;

    public void OnTouchDown()
    {
        if (OnPathTouched != null)
            OnPathTouched(this);
    }

}
