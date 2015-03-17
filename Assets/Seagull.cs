using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {

    public float Speed = 2;

    //How long before gull tries for an egg
    public float Delay = 1;

    #region MonoBehaviour

    // Use this for initialization
	void Start () {
        _lastAttack = Time.time;
        Egg.EggPickedUp += EggGrabbed;
	}

	
	// Update is called once per frame
	void FixedUpdate () {

        if( _target != null)
            transform.position += (_target.position - transform.position).normalized * Speed * Time.deltaTime;
        else
            if (Time.time >= _lastAttack + Delay)
            {
                _lastAttack = Time.time;
                FindTargetEgg();
            }
	}

    void OnDestroy()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "egg")
        {
            Egg hit = col.GetComponent<Egg>();

            if (hit != _targetEgg)
                return;

            _targetEgg.Kill();

            _target = null;
            _targetEgg = null;
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "egg")
        {
            Egg hit = col.GetComponent<Egg>();

            if (hit != _targetEgg)
                return;

            _targetEgg.Kill();

            _target = null;
            _targetEgg = null;
        }

    }

    #endregion

    private void FindTargetEgg()
    {
        Egg egg;
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("egg");        
        for(int i = 0; i < eggs.Length; ++i){
            egg = eggs[i].GetComponent<Egg>();

            if (egg != null && !egg.BeingCarried)
            {
                _target = eggs[i].transform;
                _targetEgg = egg;
            }
        }

    }

    private void EggGrabbed(Egg egg)
    {
        Egg target = _target.GetComponent<Egg>();
        if(target == egg)
        {
            _target = null;
            FindTargetEgg();
        }
    }

    private Transform _target;
    private Egg _targetEgg;
    private float _lastAttack;
}
