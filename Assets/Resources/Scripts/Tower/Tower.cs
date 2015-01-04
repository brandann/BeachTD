/* Base class for all Towers 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour {

    public enum TowerState { Idle, Acting, Disabled };

    //Cost of building 
    public int Cost { get; private set; }

    //Time between firing
    public float CoolDownTime { get; protected set; }
    
    //Current state of the tower
    public TowerState State { get; protected set;}

    //Store possible targets  
    protected List<EnemyBehavior> Targets; //todo collection should be of enemy base class

    //Decide which target to attack
    protected abstract void PrioritizeTargets();

    //Take relevant action (attack, slow etc, deploy troops etc)
    protected abstract void Act();

    //Animator Controller
    protected Animator mAnim;

    protected virtual void Start()
    {
        mAnim = gameObject.GetComponent<Animator>();
        
        if(mAnim == null)
            Debug.LogError("Missing animator");

        Targets = new List<EnemyBehavior>();
    }

    //Timestamp of last action
    protected float LastActionTime;
    
    //Time available to act again
    protected float NextActionTime;

    
}
