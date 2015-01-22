using UnityEngine;
using System.Collections;

public class TipBarnacle : MonoBehaviour {

    private enum TipState { Idle, Attacking, Retracting };
    private TipState State;

    public Transform Target { get; protected set; }
    private Transform _base;
    private float _locationEpsilon = 0.15f;
    private float _kinematicSpeed = 2.0f;
    private Rigidbody2D[] _links;

	// Use this for initialization
	void Start () {
        _base = gameObject.GetComponentInParent<Tower>().gameObject.transform;
        if (_base == null)
            Debug.Log("Missing base");      
	}
	
	// Update is called once per frame
	void Update () {

        switch (State)
        {
            case TipState.Idle:
                break;
            case TipState.Retracting:
                PhysicsFinishRetracting();
                break;
            case TipState.Attacking:
                KinematicMoveTowardTarget();
                break;
        }
	}

    public void Attack(Transform targ)
    {
        State = TipState.Attacking;
        Target = targ;
    }

    private void PhysicsFinishRetracting()
    {
        if (State == TipState.Retracting && 
           (transform.position - _base.position).magnitude <= _locationEpsilon)
        {
            Debug.Log("Retraction complete");
            gameObject.rigidbody2D.velocity = Vector2.zero;
            gameObject.rigidbody2D.isKinematic = true;
            State = TipState.Idle;
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
        transform.position += (Vector3)(dir2Target * _kinematicSpeed * Time.deltaTime);

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

        if (rigidbody2D.isKinematic)
        {
            rigidbody2D.isKinematic = false;
        }

        gameObject.rigidbody2D.velocity = Vector2.zero;
        Vector2 dir2Base = _base.position - transform.position;
        dir2Base *= 1000;
        gameObject.rigidbody2D.AddForce(dir2Base);
        State = TipState.Retracting;
        //Debug.Log("Retracting");
    }
}
