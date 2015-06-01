﻿using UnityEngine;
using System.Collections;

public class RangedTower : Tower
{
    #region Events
    public delegate void RangedFired();
    
    public static event RangedFired OnRangedFired;

    #endregion
    public float speed;
    public GameObject ProjectilePrefab;

    protected int _spinesPerShot;

    public override void Initialize()
    {
        base.Initialize(); 
        CoolDownTime = speed;
        _spinesPerShot = 1;
        gameObject.SetActive(false);
    }    

    /// <summary>
    /// Fire Tower Action creates a go and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        base.Act();

        //Create a spine for each enemy in rane up to spines per shot
        int spinesToCreate = Mathf.Min(_targets.Count, _spinesPerShot);

        for(int i = 0; i < spinesToCreate; ++i)
        {
            if (_targets[i] == null)
                continue;

            GameObject target = _targets[i].gameObject;

            GameObject arrow = GameObject.Instantiate(ProjectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

            Projectile projectile = arrow.GetComponent<Projectile>();

            projectile.setTarget(target.transform);

            GetComponentInChildren<TurretShootBounce>().Shoot();
        }

        if (OnRangedFired != null)
            OnRangedFired();
        //Debug.Log("Act");
    }

    protected override void UpgradeSpecial(int level)
    {
        _spinesPerShot += level;        
    }
    
}

