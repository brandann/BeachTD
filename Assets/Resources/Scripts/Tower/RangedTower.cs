using UnityEngine;
using System.Collections;

public class RangedTower : Tower
{

    public GameObject ProjectilePrefab;

    public override void DisableTower()
    {
        TransitionToState(TowerState.Disabled);
    }

    public override void EnableTower()
    {
        TransitionToState(_previousState);
    }

    protected override void TransitionToState(Tower.TowerState toState)
    {           
        CurrentState = toState;
       
        UpdateAnimator();  
    }

    protected override void UpdateAnimator()
    {
        switch (CurrentState)
        {
            case TowerState.Disabled:
            case TowerState.Idle:
                _anim.SetTrigger(_stopHash);
                break;
        }
    }

    protected override void Start()
    {
        base.Start();
        _flashHash = Animator.StringToHash("Flash");
        _stopHash = Animator.StringToHash("Stop");
        CoolDownTime = 0.5f;
    }

    void Update()
    {
        if (CurrentState != TowerState.Acting)
            return;

        if (Time.time >= _nextActionTime)
            Act();
    }

    /// <summary>
    /// Fire Tower Action creates a projectile and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        if (Targets.Count == 0)
        {

            TransitionToState(TowerState.Idle);
            return;
        }

        
        _lastActionTime = Time.time;
        _nextActionTime = _lastActionTime + CoolDownTime;

        _anim.SetTrigger(_flashHash);
        Debug.Log("Act");
    }

    protected override void PrioritizeTargets()
    {
        //Need to sort by distance to eggs
        //Targets.Sort( delegate (EnemyBehavior
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBehavior eb = other.gameObject.GetComponent<EnemyBehavior>();

        //Ignore collisions with non-enemies
        if (eb == null)
            return;

        Targets.Add(eb);

        PrioritizeTargets();

        if (CurrentState == TowerState.Idle)
            TransitionToState(TowerState.Acting);

        //Debug.Log("Added Enemy to targets");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyBehavior eb = other.gameObject.GetComponent<EnemyBehavior>();
        if (eb == null)
            return;

        Targets.Remove(eb);
        Debug.Log("Removed Enemy from targets");

    }

    //Animator Triggers
    private int _flashHash;
    private int _stopHash;
}

