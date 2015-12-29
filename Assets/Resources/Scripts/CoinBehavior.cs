using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {

    public delegate void CoinCompleted(int value);
    public static event CoinCompleted CoinReachedEnd;

    private Vector3 mDestination;
    private bool _move;
    private int _value;

	// Use this for initialization
	void Start () {
        mDestination = new Vector3(4.5f, 0f, 0);
        //this.transform.LookAt(mDestination);
        _move = false;
        setRotation();
        
	}
	
	// Update is called once per frame
	void Update () {

        var distance = (this.transform.position - this.mDestination).magnitude;
        if (distance > .1f)
        {
            this.transform.position += (4f * distance) * Time.smoothDeltaTime * transform.up; // move torwards the destination
        }
        else
        {
            if (CoinReachedEnd != null)
                CoinReachedEnd(_value);
            GameObject.Destroy(this.gameObject); // destroy and add to the funds
        }
	}

    private void setRotation()
    {
        var nextPoint = mDestination;
        transform.up = nextPoint - transform.position;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }

    public void setValue(int value)
    {
        _value = value;
    }
}
