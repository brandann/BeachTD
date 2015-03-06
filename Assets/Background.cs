using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{

    #region Events
    public delegate void BackgroundTouched();
    public static event BackgroundTouched OnBackgroundTouched;

    #endregion

    void OnTouchDown()
    {
        if (OnBackgroundTouched != null)
            OnBackgroundTouched();

    }
}
