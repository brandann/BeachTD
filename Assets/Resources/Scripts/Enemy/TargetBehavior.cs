using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour {

    //EDITOR MEMBERS
    public float mStartSize;
    public float mEndSize;
    public float mStartSpeed;
    public float mEndSpeed;

    //PRIVATE MEMEBERS
    private FanRotation mFan;
    public bool mActive;
    private SpriteRenderer mSpriteRender;
    private float mRate;

	// Use this for initialization
	void Start () {
        mFan = this.gameObject.GetComponent<FanRotation>();
        mSpriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        mRate = 0.1f;
        _setStatus(mStartSpeed, mStartSize);
        hide();
	}
	
	// Update is called once per frame
	void Update () {
        if (mActive)
        {
            float currentSize = this.transform.localScale.x;
            float currentSpeed = this.mFan.RotationSpeed;
            if(mEndSize - currentSize > -.1f)
            {
                //end the shrinking
                _setStatus(mEndSpeed, mEndSize);
                mActive = false;
            }
            else
            {
                // shrink the target
                float size = currentSize + this.mRate * (this.mEndSize - currentSize);
                float speed = currentSpeed + this.mRate * (this.mEndSpeed - currentSpeed);

                _setStatus(speed, size);
            }
        }
	}

    public void show()
    {
        mActive = true;

        _setStatus(mStartSpeed, mStartSize);
        this.mSpriteRender.enabled = mActive;
    }

    public void hide()
    {
        mActive = false;

        this.mSpriteRender.enabled = mActive;
    }

    private void _setStatus(float speed, float scale)
    {
        mFan.RotationSpeed = speed;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
