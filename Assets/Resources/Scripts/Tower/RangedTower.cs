using UnityEngine;
using System.Collections;

public class RangedTower : Tower
{

    public GameObject ProjectilePrefab;

    protected int _spinesPerShot;

    public override void Initialize()
    {
        base.Initialize(); 
        CoolDownTime = 1f;
        _spinesPerShot = 1;
    }    

    /// <summary>
    /// Fire Tower Action creates a arrow and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        base.Act();

        //Create a spine for each enemy in rane up to spines per shot
        int spinesToCreate = Mathf.Min(_targets.Count, _spinesPerShot);

        for(int i = 0; i < spinesToCreate; ++i)
        {
            GameObject target = _targets[i].gameObject;

            GameObject arrow = GameObject.Instantiate(ProjectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

            Projectile projectile = arrow.GetComponent<Projectile>();

            projectile.setTarget(target.transform);
        }        
        
        //Debug.Log("Act");
    }

    protected override void UpgradeSpecial(int level)
    {
        _spinesPerShot = level;        
    }
    
}

