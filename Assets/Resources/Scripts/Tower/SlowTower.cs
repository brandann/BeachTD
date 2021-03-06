﻿using UnityEngine;
using System.Collections;

public class SlowTower : Tower
{
    #region Events
    public delegate void SlowFired();

    public static event SlowFired OnSlowFired;

    #endregion

    public GameObject ProjectilePrefab;
    public float SlowCoolDownTime = .5f;

    public float BaseDuration { get; protected set; }

    protected float _durationMultiplier = 1;
    

    public override void Initialize()
    {
        base.Initialize(); 
        CoolDownTime = SlowCoolDownTime;
        gameObject.SetActive(false);
    }    

    /// <summary>
    /// Fire Tower Action creates a go and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        base.Act();

        if (_targets.Count == 0)
            return;

        GameObject target = this.getTarget().gameObject;

        if (target == null)
            return;

        GameObject go = GameObject.Instantiate(ProjectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

        SlowProjectile slowPro = go.GetComponent<SlowProjectile>();

        slowPro.setTarget(target.transform);

        //slowPro.Duration = BaseDuration * _durationMultiplier;        
        
        //Debug.Log("Act");

        if (OnSlowFired != null)
            OnSlowFired();
    }

    protected override void UpgradeSpecial(int level)
    {
        _durationMultiplier = 1 + (level * 0.1f);
    }
    
}