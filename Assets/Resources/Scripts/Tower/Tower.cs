/* Base class for all Towers 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{
    #region public inteface
    public enum TowerState { Idle, Acting, Disabled };

    //Cost of building 
    public int Cost { get; private set; }

    //Time between firing
    public float CoolDownTime { get; protected set; }
    
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

    #endregion

    //Animator Triggers
    protected readonly int _flashHash = Animator.StringToHash("Flash");
    protected readonly int _stopHash = Animator.StringToHash("Stop");

    void Update()
    {
        if (CurrentState != TowerState.Acting)
            return;

        if (Time.time >= _nextActionTime)
            Act();
        else
            TransitionToState(TowerState.Idle);
    }


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
    void HandleEnemyDeath(Enemy enemy)
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

    protected virtual void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        
        if(_anim == null)
            Debug.LogError("Missing animator");

        _targets = new List<Enemy>();
        
    }

    

   

    
}
