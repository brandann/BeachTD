using UnityEngine;
using System.Collections;

public class MeleeTower : Tower {
    
	// Use this for initialization
    protected override void Start()
    {
        base.Start();
        CoolDownTime = 5;
        _tip = GetComponentInChildren<TipBarnacle>();
        _tip.SetDamage(Damage);
        Damage = 5;
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

    protected override void UpgradeSpecial(int level)
    {
        Debug.LogWarning("Barnacle has no special to upgrade yet");
    }
    

    private TipBarnacle _tip;


}
