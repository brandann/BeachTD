﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private float SnapGrid = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getScale()
	{
		return SnapGrid;
	}
}
