using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {

    public delegate void GullKilled(Seagull gull);
    public static event GullKilled OnGullKilled;

    public int GullKillValue = 10;
    public float Speed = 2;

    //How long before gull tries for an egg
    public float Delay = 10;


    public void OnTouchDown()
    {
        if(OnGullKilled != null)
            OnGullKilled(this);

        Initialize();
    }
    

    #region MonoBehaviour

    // Use this for initialization
	void Start () {
        Initialize();
        Egg.EggPickedUp += EggGrabbed;
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
                    transform.position += (_target.position - transform.position).normalized * Speed * Time.deltaTime;
                else
                    FindTargetEgg();
                break;
            case BirdBrain.Fleeing:
                transform.position += (_endPoint - transform.position).normalized * Speed * Time.deltaTime;
				transform.right = _endPoint - transform.position;
                if ((Time.time - _endTime) >= _restartDelay)
                {
                    Initialize();
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
        HandleEggCollision(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        HandleEggCollision(col);
    }

    private void HandleEggCollision(Collider2D col)
    {
        if (_state == BirdBrain.Fleeing)
            return;

        if (col.gameObject.tag == "egg")
        {
            Egg hit = col.GetComponent<Egg>();

            if (hit != _targetEgg)
                return;

            _targetEgg.Kill();

            _target = null;
            _targetEgg = null;

            _state = BirdBrain.Fleeing;

            _endPoint = new Vector3(18, Random.Range(-1, 20), 0);
            _endTime = Time.time;
            _restartDelay = Random.Range(5, 12);
        }
    }

    void OnBecameInvisible()
    {
        Initialize();
    }

    

    #endregion



    private void Initialize()
    {
        _state = BirdBrain.Idle;
        _startPoint = new Vector3(-1, Random.Range(0, 10), 0);
        transform.position = _startPoint;
        _lastAttack = Time.time;
    }

    private void FindTargetEgg()
    {
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
                if (_target == null)
                    _state = BirdBrain.Fleeing;
                break;
            case BirdBrain.Idle:
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
        //Can't be our egg if we don't have a target
        if (_target == null)
            return;

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
    private float _restartDelay;
    private float _endTime;
    private Vector3 _startPoint;
    private Vector3 _endPoint = new Vector3(18, 5, 0);
    private BirdBrain _state;

    private enum BirdBrain { Idle, Attacking, Fleeing };

   
   
}
