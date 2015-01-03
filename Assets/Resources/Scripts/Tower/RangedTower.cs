using UnityEngine;
using System.Collections;

public class RangedTower : Tower {

    protected override void Start()
    {
        base.Start();
        mFlashHash = Animator.StringToHash("Flash");
        mStopHash = Animator.StringToHash("Stop");
        StartCoroutine(Act());
    }

    void Update()
    {
       
    }

 

    protected override IEnumerator Act()
    {
        while (State == TowerState.Active)
        {
            mAnim.SetTrigger(mFlashHash);
            yield return new WaitForSeconds(CoolDownTime);
        }

        mAnim.SetTrigger(mStopHash);
    }

    protected override void PrioritizeTargets()
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBehavior em = other.gameObject.GetComponent<EnemyBehavior>();
        
        //Ignore collisions with non-enemies
        if (em == null)
            return;

        Targets.Add(other.gameObject);
        Debug.Log("Added Enemy to targets");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyMovement em = other.gameObject.GetComponent<EnemyMovement>();
        if (em == null)
            return;

        Targets.Remove(em.gameObject);
        Debug.Log("Removed Enemy from targets");
    }

    private int mFlashHash;
    private int mStopHash;
	
}
