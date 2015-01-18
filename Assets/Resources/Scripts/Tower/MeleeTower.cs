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

        GameObject target = _targets[0].gameObject;
        Vector2 dir2Targ = target.transform.position - _tip.transform.position;
        dir2Targ.Normalize();
        dir2Targ *= 1000;
        Debug.Log("Appling " + dir2Targ + " to tip");
        _tip.rigidbody2D.velocity = Vector2.zero;
        _tip.rigidbody2D.AddForce(dir2Targ);
        _tip.Target = target.transform;
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
