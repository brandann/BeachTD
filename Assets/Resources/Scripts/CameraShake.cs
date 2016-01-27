using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform mCameraTransform;

    // How long the object should shake for.
    public float mShake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float mShakeAmount = 0.7f;
    public float mDecreaseFactor = 1.0f;

    Vector3 _mOriginalPos;

    void Awake()
    {
        if (mCameraTransform == null)
        {
            mCameraTransform = GameObject.Find("Main Camera").GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        _mOriginalPos = mCameraTransform.localPosition;
    }

    void Update()
    {
        if(mShake != 0 && Time.deltaTime == 0)
        {
        	mShake = 0;
        }
        else if (mShake > 0)
        {
            mCameraTransform.localPosition = _mOriginalPos + Random.insideUnitSphere * mShakeAmount;

            mShake -= Time.deltaTime * mDecreaseFactor;
        }
        else
        {
            mShake = 0f;
            mCameraTransform.localPosition = _mOriginalPos;
        }
    }

    public void Shake()
    {
        Shake(0.1f);
    }

    public void Shake(float f)
    {
        mShake = f;
    }
}
