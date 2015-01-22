using UnityEngine;
using System.Collections;

public class MeleeTower : Tower {
    public float Damage = 5;

    
	// Use this for initialization
	void Start () {
        base.Start();
        CoolDownTime = 5;
        _tip = GetComponentInChildren<TipBarnacle>();
        _tip.SetDamage(Damage);
	}

    protected override void Act()
    {
        base.Act();
        _tip.Attack(_targets[0].transform);        
    }

    

    public override void DisableTower()
    {
        throw new System.NotImplementedException();
    }

    public override void EnableTower()
    {
        TransitionToState(_previousState);
    }

    protected override void HandleEnemyDeath(Enemy enemy)
    {
        base.HandleEnemyDeath(enemy);
        _tip.ClearTarget();
    }

    private TipBarnacle _tip;


}
