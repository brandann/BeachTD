using UnityEngine;
using System.Collections;

public class SlowTower : Tower
{

    public GameObject ProjectilePrefab;
    public readonly float SlowCoolDownTime = 1;

    public float BaseDuration { get; protected set; }

    protected float _durationMultiplier = 1;
    

    public override void Initialize()
    {
        base.Initialize(); 
        CoolDownTime = SlowCoolDownTime;
    }    

    /// <summary>
    /// Fire Tower Action creates a arrow and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        base.Act();              

        GameObject target = _targets[0].gameObject;

        GameObject arrow = GameObject.Instantiate(ProjectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

        SlowProjectile slowPro = arrow.GetComponent<SlowProjectile>();

        slowPro.setTarget(target.transform);

        slowPro.Duration = BaseDuration * _durationMultiplier;        
        
        //Debug.Log("Act");
    }

    protected override void UpgradeSpecial(int level)
    {
        _durationMultiplier = 1 + (level * 0.1f);
    }
    
}