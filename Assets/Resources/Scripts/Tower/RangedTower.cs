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
        Debug.Log("Enemy detected");
    }

	
}
