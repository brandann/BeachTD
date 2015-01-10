using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Transform Target { get; private set; }
    public float Speed = 1.5f;
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(transform.position + new Vector3(0,0,1), Target.position - transform.position);
        transform.position += transform.up * Speed * Time.deltaTime;
	}

    public void setTarget(Transform targ){
        Target = targ;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit");
        Enemy en = other.gameObject.GetComponent<Enemy>();

        if (en == null)
            return;

        en.TakeDamage(5);
        Destroy(gameObject);
    }

    //Straight line between current position and target's position
    private Vector3 lineToTarg;
}
