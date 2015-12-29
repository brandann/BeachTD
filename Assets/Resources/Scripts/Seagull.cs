﻿using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {

    public delegate void GullKilled(Seagull gull);
    public static event GullKilled OnGullKilled;
    public CameraShake mCameraShake;

    public int GullKillValue;
    public float Speed;
    private float DelayMin = 5;
    private float DelayMax = 12;

    //How long before gull tries for an egg
    public float Delay;

    #region MonoBehaviour
    public void OnTouchDown()
    {
        if(OnGullKilled != null)
            OnGullKilled(this);

        Initialize(BirdBrain.Idle);
    }
    
    // Use this for initialization
	void Start () {
        Initialize(BirdBrain.Init);
        Egg.EggPickedUp += EggGrabbed;
        mCameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
	}    

	
	// Update is called once per frame
	void FixedUpdate () {

        switch (_state)
        {
            case BirdBrain.Idle:
                if (Time.time >= _lastAttack + Delay)
                {
                    _lastAttack = Time.time;
                    FindTargetEgg();
                }
                break;
            case BirdBrain.Attacking:
                if( _target != null)
                {
                    transform.position += (_target.position - transform.position).normalized * Speed * Time.deltaTime;
                }
                    
                else
                {
                    print("GULL");
                    FindTargetEgg();
                }
                    
                break;
            case BirdBrain.Fleeing:
                print("GULL");
                transform.position += (_endPoint - transform.position).normalized * Speed * Time.deltaTime;
				transform.right = _endPoint - transform.position;
                if ((Time.time - _endTime) >= _restartDelay)
                {
                    Initialize(BirdBrain.Idle);
                }
                break;                
        } 
	}

    void OnDestroy()
    {
        Egg.EggPickedUp -= EggGrabbed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("GULL");
        HandleEggCollision(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        print("GULL");
        HandleEggCollision(col);
    }

    private void HandleEggCollision(Collider2D col)
    {
        print("GULL");
        if (_state == BirdBrain.Fleeing)
            return;
        print("GULL");
        if (col.gameObject.tag == "egg")
        {
            print("GULL");
            Egg hit = col.GetComponent<Egg>();

            if (hit != _targetEgg)
                return;
            print("GULL");
            _targetEgg.Kill();

            _target = null;
            _targetEgg = null;

            _state = BirdBrain.Fleeing;

            _endPoint = new Vector3(18, Random.Range(-1, 20), 0);
            _endTime = Time.time;
            Delay = _restartDelay = Random.Range(DelayMin, DelayMax);

            mCameraShake.Shake();
        }
    }

    void OnBecameInvisible()
    {
        print("GULL");
        Initialize(BirdBrain.Idle);
    }

    #endregion

    private void Initialize(BirdBrain bb)
    {
        _state = bb;
        _startPoint = new Vector3(-4, Random.Range(0, 10), 0);
        transform.position = _startPoint;
        _lastAttack = Time.time;
    }

    public void StartPlane()
    {
        if (_state == BirdBrain.Init)
        {
            _state = BirdBrain.Idle;
            _lastAttack = Time.time;
        }
    }

    private void FindTargetEgg()
    {
        //print("GULL");
        Egg egg;
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("egg");        
        for(int i = 0; i < eggs.Length; ++i){
            egg = eggs[i].GetComponent<Egg>();

            if (egg != null && !egg.BeingCarried)
            {
                _target = eggs[i].transform;
                transform.right = eggs[i].transform.position - transform.position;
                _targetEgg = egg;
            }
        }

        switch (_state)
        {
            case BirdBrain.Attacking:
                print("GULL");
                if (_target == null)
                    _state = BirdBrain.Fleeing;
                break;
            case BirdBrain.Idle:
                //print("GULL");
                if (_target != null)
                    _state = BirdBrain.Attacking;
                break;
            default:
                Debug.LogWarning("Should be called");
                break;                
        }

    }

    private void EggGrabbed(Egg egg)
    {
        //print("GULL");
        //Can't be our egg if we don't have a target
        if (_target == null)
            return;
        //print("GULL");
        Egg target = _target.GetComponent<Egg>();
        if(target == egg)
        {
            print("GULL");
            _target = null;
            FindTargetEgg();
        }

        Delay = Random.Range(DelayMin, DelayMax);
    }

    private Transform _target;
    private Egg _targetEgg;
    private float _lastAttack;
    private float _restartDelay;
    private float _endTime;
    private Vector3 _startPoint;
    private Vector3 _endPoint = new Vector3(18, 5, 0);
    private BirdBrain _state;

    private enum BirdBrain { Idle, Attacking, Fleeing, Init };

   
   
}
