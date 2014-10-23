using UnityEngine;
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
		Destroy(this.gameObject);
	}
}
