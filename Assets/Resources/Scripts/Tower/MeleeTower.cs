using UnityEngine;
using System.Collections;

public class MeleeTower : Tower {
    public float Damage = 5;


	// Use this for initialization
	void Start () {
        CoolDownTime = 3;	
	}

    protected override void Act()
    {
        throw new System.NotImplementedException();
    }

    public override void DisableTower()
    {
        throw new System.NotImplementedException();
    }

    public override void EnableTower()
    {
        TransitionToState(_previousState);
    }


}
