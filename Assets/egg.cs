using UnityEngine;
using System.Collections;

public class egg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision");
		if(collision.gameObject.tag == "enemy")
		{
			Debug.Log ("enemy collision");
		}
	}
}
