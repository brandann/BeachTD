using UnityEngine;
using System.Collections;

public class OpenAreaBehavior : MonoBehaviour {

    public delegate void OpenAreaHandler(OpenAreaBehavior area);

    public static event OpenAreaHandler onAreaTouched;

    public void OnTouchDown()
    {
        if (onAreaTouched != null)
        {
            onAreaTouched(this);
        }
    }

}
