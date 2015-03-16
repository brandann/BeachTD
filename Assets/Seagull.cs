using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {

    public float Speed = 3;


	// Use this for initialization
	void Start () {

        GameObject go = GameObject.Find("egg");
        if(go != null)
            _target = go.transform.position;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if( _target != null)
            transform.position += (_target - transform.position) * Speed * Time.deltaTime;
	}

    private Vector3 _target;
}
