using UnityEngine;
using System.Collections;

public class WarningUI : MonoBehaviour {

    private float mAwakeTime;
    private float mDuration;

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        setWarningActive();
    }
	
	// Update is called once per frame
	void Update () {
	    if((Time.timeSinceLevelLoad - mAwakeTime) >= mDuration)
        {
            this.gameObject.SetActive(false);
        }
	}

    public void setWarningActive()
    {
        mAwakeTime = Time.timeSinceLevelLoad;
        mDuration = 2;
    }
}
