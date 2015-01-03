using UnityEngine;
using System.Collections;

public class RangedTower : Tower {

    protected override void Act()
    {
        throw new System.NotImplementedException();
    }

    protected override void PrioritizeTargets()
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyMovement em = other.gameObject.GetComponent<EnemyMovement>();
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

	
}
