﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Collider2D Target;
    public float Speed;
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

    }
}
