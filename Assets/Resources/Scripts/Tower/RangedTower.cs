using UnityEngine;
using System.Collections;

public class RangedTower : Tower {

    protected override void Start()
    {
        base.Start();
        mFlashHash = Animator.StringToHash("Flash");
        mStopHash = Animator.StringToHash("Stop");
    }

    void Update()
    {
       
    }

    protected override void Act()
    {       
        mAnim.SetTrigger(mFlashHash);            
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

        Debug.Log("Added Enemy to targets");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyBehavior eb = other.gameObject.GetComponent<EnemyBehavior>();
        if (eb == null)
            return;

        Targets.Remove(eb);
        Debug.Log("Removed Enemy from targets");

        switch (State)
        {
            case TowerState.Disabled:
                return;
            case TowerState.Idle:
            case TowerState.Acting:
                PrioritizeTargets();
                break;
            default:
                Debug.LogError("What state are you in?");
                break;
        }
    }

    //Animator Triggers
    private int mFlashHash;
    private int mStopHash;

    //Indicates ranged tower is firing or waiting to fire at a target used to start and stop coroutine
    private bool mFiring;
}
