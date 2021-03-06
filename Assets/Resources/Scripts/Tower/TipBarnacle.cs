﻿using UnityEngine;
using System.Collections;

public class TipBarnacle : MonoBehaviour {

    private enum TipState { Idle, Attacking, Retracting };
    private TipState State;
    public float Damage { get; protected set; }
    public Transform Target { get; protected set; }
    private Transform _baseTransform;
    private MeleeTower _baseTower;
    private float _locationEpsilon = 0.15f;
    private float _kinematicSpeed = 2.0f;
    private Rigidbody2D[] _links;
    private bool _hitTarget;
    public float damage;

	// Use this for initialization
	void Start () {
        Damage = damage;
        _baseTower = gameObject.GetComponentInParent<MeleeTower>();
        _baseTransform = _baseTower.gameObject.transform;
        
        if (_baseTransform == null)
            Debug.LogWarning("Missing base");
        GetComponent<Rigidbody2D>().isKinematic = true;
        PhysicsRetract();
	}

    void OnDestroy()
    {
        //Debug.Log("shouldn't be destroyed");
    }

    void OnDisable()
    {
        //Debug.Log("tip disabled");
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

    public void Attack(Transform targ, float speed)
    {
        _kinematicSpeed = speed;
        State = TipState.Attacking;
        Target = targ;
        _hitTarget = false;
    }

    public void ClearTarget()
    {
        Target = null;
        PhysicsRetract();
    }

    private void PhysicsFinishRetracting()
    {
        if (State == TipState.Retracting &&
           (transform.position - _baseTransform.position).magnitude <= _locationEpsilon)
        {
            //Debug.Log("Retraction complete");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            State = TipState.Idle;
        }
        else
        {
            Vector2 dir2Target = _baseTransform.position - transform.position;
            dir2Target.Normalize();
            GetComponent<Rigidbody2D>().AddForce(dir2Target * 10, ForceMode2D.Impulse);
            //Debug.Log("retracting force: " + dir2Target * 10);
        }       

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">Damage applied to Target's Enemy. Negative values floored at 0.</param>
    public void SetDamage(float damage)
    {
        Damage = Mathf.Min(0,damage);

    }

    protected void KinematicMoveTowardTarget()
    {

        if (null == Target)
        {
            PhysicsRetract();
            return;
        }

        Vector2 distFromBase = transform.position - _baseTransform.transform.position;
        if (distFromBase.magnitude > _baseTower.Range)
        {
            Target = null;
            PhysicsRetract();
            return;
        }
            

        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        
        Vector2 dir2Target = (Target.position - transform.position).normalized;
        transform.position += (Vector3)(dir2Target * _kinematicSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * Time.deltaTime * 1000, Space.World);

    }


    public void PhysicsJumpTowardTarget()
    {
        
        Vector2 dir2Targ = Target.position - transform.position;
        dir2Targ.Normalize();
        dir2Targ *= 1000;
        //Debug.Log("Appling " + dir2Targ + " to tip");
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(dir2Targ);
    }

    private void PhysicsRetract()
    {
        //Debug.Log("PhysicsRetract");

        if (GetComponent<Rigidbody2D>().isKinematic)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 dir2Base =  _baseTransform.position - transform.position;
        dir2Base.Normalize();
        dir2Base *= 100;
        //Debug.Log("Rectract Apply: " + dir2Base); 
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir2Base);
        State = TipState.Retracting;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform != Target || _hitTarget)
            return;

        _hitTarget = true;
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(Damage);

        PhysicsRetract();
        Target = null;

    }
}
