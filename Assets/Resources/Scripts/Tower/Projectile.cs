using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Transform Target { get; protected set; }
    public float Speed = 1.5f;
    public float Damage = 4;
	
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


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit");
        Enemy en = other.gameObject.GetComponent<Enemy>();

        //Ignore non-enemies and non-targets
        if (en == null || other.transform != Target)
            return;

        en.TakeDamage(Damage);
        Destroy(gameObject);
    }

    //Straight line between current position and target's position
    private Vector3 lineToTarg;
}
