using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAim : MonoBehaviour {

    public GameObject Tower;
    List<Enemy> _targets;

	// Use this for initialization
	void Start () {
        _targets = Tower.GetComponent<RangedTower>().Targets;
	}
	
	// Update is called once per frame
	void Update () {

        if (_targets != null && _targets.Count > 0 && _targets[0] != null)
        {
            GameObject target = _targets[0].gameObject;

            Vector3 lookPos = target.transform.position;
            lookPos = lookPos - transform.position;
            transform.up = lookPos;
        }
	}
}
