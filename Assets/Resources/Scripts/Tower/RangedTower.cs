using UnityEngine;
using System.Collections;

public class RangedTower : Tower
{

    public GameObject ProjectilePrefab;
    

    protected override void Start()
    {
        base.Start(); 
        CoolDownTime = 1f;
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

        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.setTarget(target.transform);
        
        //Debug.Log("Act");
    }    
    
}

