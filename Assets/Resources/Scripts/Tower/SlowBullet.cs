using UnityEngine;
using System.Collections;

public class SlowBullet : MonoBehaviour {

    public float Speed;

	// Use this for initialization
	void Start () {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        transform.up = lookPos;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * Speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > 10)
        {
            Destroy(this.gameObject);
        }
	}
}
