using UnityEngine;
using System.Collections;

public class MeleeTower : Tower
{

    #region Events
    public delegate void MeleeFired();

    public static event MeleeFired OnMeleeFired;
    
    #endregion 

    // Use this for initialization
	public override void Initialize() {
        base.Initialize();

        CoolDownTime = 5;
        _tip = GetComponentInChildren<TipBarnacle>();
        _tip.SetDamage(Damage);
        Damage = 5;
        gameObject.SetActive(false);
	}

    protected override void Act()
    {
        base.Act();
        
        if (_targets.Count == 0)
            return;

        Enemy target = _targets[0];

        if(target == null)
            return;

        _tip.Attack(target.transform);

        if (OnMeleeFired != null)
            OnMeleeFired();
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
