using UnityEngine;
using System.Collections;

public class OpenAreaBehavior : MonoBehaviour {

    public delegate void OpenAreaTouched(OpenAreaBehavior area);

    public static event OpenAreaTouched onAreaTouched;

    public void OnTouchDown()
    {
        if (onAreaTouched != null)
        {
            onAreaTouched(this);
        }
    }

}
