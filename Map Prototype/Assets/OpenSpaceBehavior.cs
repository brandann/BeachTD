﻿using UnityEngine;
using System.Collections;

public class OpenSpaceBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTouchDown()
	{
		string prefab;
		if(Random.Range(0,2) == 0)
			prefab = "Prefabs/TowerA";
		else
			prefab = "Prefabs/TowerB";
			
		float offset = .5f;
		
		//GameObject towerPrefab = Resources.Load(prefab) as GameObject;
		//GameObject e = Instantiate(towerPrefab) as GameObject;
		//e.transform.position = this.transform.position;
		
		// make towerA purchase
		GameObject AB = Resources.Load("Prefabs/TowerABuy") as GameObject;
		GameObject ABe = Instantiate(AB) as GameObject;
		Vector3 ABp = this.transform.position + (new Vector3(-offset,offset,0));
		ABe.transform.position = ABp;
		
		// make towerB purchase
		GameObject BB = Resources.Load("Prefabs/TowerBBuy") as GameObject;
		GameObject BBe = Instantiate(BB) as GameObject;
		Vector3 BBp = this.transform.position + (new Vector3(offset,offset,0));
		BBe.transform.position = BBp;
		
		Destroy(this.gameObject);
		
	}
	
	void destroy()
	{
		Destroy(this.gameObject);
	}
}
