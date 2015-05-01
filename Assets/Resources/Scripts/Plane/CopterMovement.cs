using UnityEngine;
using System.Collections;

public class CopterMovement : MonoBehaviour
{

    public float Speed; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        transform.up = lookPos;

        transform.position += transform.up * Speed * Time.deltaTime;
	}
}
