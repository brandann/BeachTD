﻿using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Destroy myself
	public void destroy()
	{
		Destroy(this.gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("hit");
	}
}
