using UnityEngine;
using System.Collections;

public class BuyTowerBehavior : MonoBehaviour {

	Vector3 BuildPosition;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void buildPosition(Vector3 pos)
	{
		BuildPosition = pos;
	}
	
	void OnTouch()
	{
	
	}
}
