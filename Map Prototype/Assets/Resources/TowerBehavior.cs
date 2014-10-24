using UnityEngine;
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
	
	void OnTouchDown()
	{
		GameObject towerPrefab = Resources.Load("Prefabs/OpenArea") as GameObject;
		GameObject e = Instantiate(towerPrefab) as GameObject;
		e.transform.position = this.transform.position;
		Destroy(this.gameObject);
	}
}
