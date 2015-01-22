using UnityEngine;
using System.Collections;

public class MeleeTower : Tower {
    public float Damage = 5;

    
	// Use this for initialization
	void Start () {
        base.Start();
        CoolDownTime = 5;
        _tip = GetComponentInChildren<TipBarnacle>();
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

    private TipBarnacle _tip;


}
