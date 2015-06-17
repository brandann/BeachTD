using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

    public GameObject bullet;

    private float timeElapsed = 0;
    private float SpawnTime = .1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*
        if (Input.GetButton("Fire2"))
        {
            if(Time.timeSinceLevelLoad - timeElapsed > SpawnTime)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                timeElapsed = Time.timeSinceLevelLoad;
            }
            
        }
        */
	}
}
