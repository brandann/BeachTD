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
    /// Change state to Disabled
    /// </summary>
    public abstract void DisableTower();

    /// <summary>
    /// Changes state from Disabled to either Idle or Acting
    /// </summary>
    public abstract void EnableTower();

    #endregion

    protected abstract void TransitionToState(TowerState toState);

    protected abstract void UpdateAnimator();
       
    //Timestamp of last action
    protected float _lastActionTime;

    //Time available to act again
    protected float _nextActionTime;

    //Animator Controller
    protected Animator _anim;

    //Store possible targets  
    protected List<Enemy> Targets; //todo collection should be of enemy base class

    /// <summary>
    /// CurrentState of tower before last state transition
    /// </summary>
    protected TowerState _previousState;

    protected TowerState _currentState;

    /// <summary>
    /// Decide which target to attack
    /// </summary>
    protected abstract void PrioritizeTargets();

    /// <summary>
    /// Take relevant action (attack, slow etc, deploy troops etc)
    /// </summary>
    protected abstract void Act();      

    protected virtual void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        
        if(_anim == null)
            Debug.LogError("Missing animator");

        Targets = new List<Enemy>();
    }

    

   

    
}
