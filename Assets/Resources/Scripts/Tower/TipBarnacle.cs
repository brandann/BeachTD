using UnityEngine;
using System.Collections;

public class TipBarnacle : MonoBehaviour {

    public Transform Target;
    private Transform _base;
    private float _locationEpsilon = 0.15f;
    private float _kinematicSpeed = 2.0f;
    private bool _retracting;
    private Rigidbody2D[] _links;

	// Use this for initialization
	void Start () {
        _base = gameObject.GetComponentInParent<Tower>().gameObject.transform;
        if (_base == null)
            Debug.Log("Missing base");
        
	}
	
	// Update is called once per frame
	void Update () {

        //PhysicsFinishRetracting();
	        
	}

    private void PhysicsFinishRetracting()
    {
        if (_retracting && (transform.position - _base.position).magnitude <= _locationEpsilon)
        {
            Debug.Log("Retraction complete");
            gameObject.rigidbody2D.velocity = Vector2.zero;
            gameObject.rigidbody2D.isKinematic = true;
            _retracting = false;

        }

    }

    public void KinematicMoveTowardTarget()
    {
        if (!rigidbody2D.isKinematic)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.isKinematic = true;
        }
        
        Vector2 dir2Target = (Target.position - transform.position).normalized;
        transform.position = dir2Target * _kinematicSpeed * Time.deltaTime;

    }

    public void PhysicsJumpTowardTarget()
    {
        Vector2 dir2Targ = Target.position - transform.position;
        dir2Targ.Normalize();
        dir2Targ *= 1000;
        Debug.Log("Appling " + dir2Targ + " to tip");
        rigidbody2D.isKinematic = false;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(dir2Targ);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform != Target)
            return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(7);

        gameObject.rigidbody2D.velocity = Vector2.zero;
        Vector2 dir2Base = _base.position - transform.position;
        dir2Base *= 1000;
        gameObject.rigidbody2D.AddForce(dir2Base);
        _retracting = true;
        Debug.Log("Retracting");
    }
}
