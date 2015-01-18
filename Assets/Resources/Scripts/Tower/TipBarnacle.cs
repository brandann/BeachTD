using UnityEngine;
using System.Collections;

public class TipBarnacle : MonoBehaviour {

    public Transform Target;
    private Transform _base;
    private float _locationEpsilon = 0.1f;
    private bool _retracting;

	// Use this for initialization
	void Start () {
        _base = gameObject.GetComponentInParent<Tower>().gameObject.transform;
        if (_base == null)
            Debug.Log("Missing base");
	}
	
	// Update is called once per frame
	void Update () {
        if ( _retracting &&  (transform.position - _base.position).magnitude <= _locationEpsilon)
        {
            Debug.Log("Retraction complete");
            gameObject.rigidbody2D.velocity = Vector2.zero;
            _retracting = false;
        }
	        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform != Target)
            return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(5);

        gameObject.rigidbody2D.velocity = Vector2.zero;
        Vector2 dir2Base = _base.position - transform.position;
        dir2Base *= 1000;
        gameObject.rigidbody2D.AddForce(dir2Base);
        _retracting = true;
        Debug.Log("Retracting");
    }
}
