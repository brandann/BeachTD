using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAim : MonoBehaviour {

    public GameObject Tower;
    private RangedTower rTower;

	// Use this for initialization
	void Start () {
        rTower = Tower.GetComponent<RangedTower>();
	}
	
	// Update is called once per frame
	void Update () {

        GameObject target = this.rTower.getCurrentTarget();
        if (target != null)
        {
            Vector3 lookPos = target.transform.position;
            lookPos = lookPos - transform.position;
            transform.up = lookPos;
        }
	}
}
