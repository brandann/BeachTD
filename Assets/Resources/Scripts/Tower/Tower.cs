/* Base class for all Towers 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour, IUpgradeable
{
    #region Public Region

    public delegate void TowerEventHandler(Tower t);

    public static event TowerEventHandler onTowerTouched;

    /// <summary>
    /// Used to modify Towers real numbers are precentages but no limits are appled. Integers are "levels" defined by derived clasess
    /// </summary>
    public struct Upgrade
    {
        public float Range;
        public float Speed;
        public float Damage;
        public int Special;

        public Upgrade(float range = 0, float speed = 0, float damage = 0, int special = 0)
        {
            Range = range;
            Speed = speed;
            Damage = damage;
            Special = special;
        }
    }    

    //Cost of building 
    public int Cost { get; protected set; }

    //Damage done per hit
    public float Damage { get; protected set; }

    //Time between firing in seconds, higher number == slower firing
    public float CoolDownTime { get; protected set; }

    public enum TowerState { Idle, Acting, Disabled };
    
    //Current state of the tower
    public TowerState CurrentState {
        get
        {
            return _currentState;
        }
        protected set
        {
            _previousState = _currentState;
            _currentState = value;
        }
    }    

    /// <summary>
    /// Change State to Disabled
    /// </summary>
    public virtual void DisableTower()
    {
        TransitionToState(TowerState.Disabled);
    }

    /// <summary>
    /// Changes state from Disabled to either Idle or Acting
    /// </summary>
    public virtual void EnableTower()
    {
        TransitionToState(_previousState);
    }

    public virtual void UpgradeTower(Upgrade upgrade)
    {
        if(upgrade.Range != 0) _collider.radius *= (1 + upgrade.Range);
        if(upgrade.Speed != 0) CoolDownTime *= -(1 + upgrade.Speed);
        if(upgrade.Damage != 0) Damage *= (1 + upgrade.Damage);
        if(upgrade.Special != 0) UpgradeSpecial(upgrade.Special);
    }

    #endregion

    //Animator Triggers
    protected readonly int _flashHash = Animator.StringToHash("Flash");
    protected readonly int _stopHash = Animator.StringToHash("Stop");
    protected CircleCollider2D _collider;

    #region MonoBehaviour Region
    void Update()
    {
        if (CurrentState != TowerState.Acting)
            return;

        if (Time.time >= _nextActionTime)
            Act();
        else
            TransitionToState(TowerState.Idle);
    }

    void Awake()
    {
        Initialize();
    }

    #endregion

    //Used to upgrade special abilities in derived classes
    protected abstract void UpgradeSpecial(int level);


    protected virtual void TransitionToState(Tower.TowerState toState)
    {
        CurrentState = toState;

        UpdateAnimator();
    }

    protected virtual void UpdateAnimator()
    {
        switch (CurrentState)
        {
            case TowerState.Disabled:
            case TowerState.Idle:
                _anim.SetTrigger(_stopHash);
                break;
        }
    }

    public virtual void OnTouchDown()
    {
        //Debug.Log("OnTouchDown from tower");

        if (onTowerTouched != null)
            onTowerTouched(this);
        else
            Debug.Log("No subscribers");
    }

    virtual public void Initialize()
    {
        _anim = gameObject.GetComponent<Animator>();

        _collider = (CircleCollider2D)gameObject.collider2D;

        _previousState = TowerState.Idle;

        _currentState = TowerState.Idle;

        if (_anim == null)
            Debug.LogError("Missing animator");

        _targets = new List<Enemy>();

       
    }
       
    //Timestamp of last action
    protected float _lastActionTime;

    //Time available to act again
    protected float _nextActionTime;

    //Animator Controller
    protected Animator _anim;

    //Store possible targets  
    protected List<Enemy> _targets;
    
    /// <summary>
    /// CurrentState of tower before last state transition
    /// </summary>
    protected TowerState _previousState;

    protected TowerState _currentState;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();

        //Ignore collisions with non-enemies
        if (eb == null)
            return;
        

        //If we are not already targeting enemy
        if (_targets.Contains(eb) == false)
        {
            _targets.Add(eb);
            eb.OnEnemyDied += HandleEnemyDeath;
        }
        else
        {
            //Enemy is already being targeted;
            return;
        }

        PrioritizeTargets();

        if (CurrentState == TowerState.Idle)
            TransitionToState(TowerState.Acting);

    }

    /// <summary>
    /// Handles EnemyDied events by removing them from target list
    /// </summary>
    /// <param name="enemy">enemy that just died</param>
    protected virtual void HandleEnemyDeath(Enemy enemy)
    {
        _targets.Remove(enemy);
        enemy.OnEnemyDied -= HandleEnemyDeath;
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();
        if (eb == null)
            return;

        _targets.Remove(eb);
        //Debug.Log("Removed Enemy from targets");
    }





    /// <summary>
    /// Decide which target to attack; Default behavior is to target enemy that has traveled the furthest
    /// </summary>
    protected virtual void PrioritizeTargets()
    {
        _targets.Sort(
            delegate(Enemy e1, Enemy e2)
            {
                float e1Dist = e1.gameObject.GetComponent<EnemyMovement>().DistanceTraveled;
                float e2Dist = e2.gameObject.GetComponent<EnemyMovement>().DistanceTraveled;
                return e2Dist.CompareTo(e1Dist);
            }
        );
    }

    /// <summary>
    /// Take relevant action (attack, slow etc, deploy troops etc)
    /// </summary>
    protected virtual void Act()
    {
        if (_targets.Count == 0){
            TransitionToState(TowerState.Idle);
            return;
        }
 
        _lastActionTime = Time.time;
        _nextActionTime = _lastActionTime + CoolDownTime;

        _anim.SetTrigger(_flashHash);
    }

    protected bool AreTargetsAvailable()
    {
        return (_targets.Count > 0);
    }

}
