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

    void Update()
    {
        if (CurrentState != TowerState.Acting)
            return;

        if (Time.time >= _nextActionTime)
            Act();
    }

    /// <summary>
    /// Fire Tower Action creates a arrow and sends it toward the highest priority target. 
    /// Updates timer for next action. If no targets exist transitions tower to Idle
    /// </summary>
    protected override void Act()
    {
        base.Act();

        if (_targets.Count == 0)
        {
            TransitionToState(TowerState.Idle);
            return;
        }       

        GameObject target = _targets[0].gameObject;

        GameObject arrow = GameObject.Instantiate(ProjectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

        Projectile projectile = arrow.GetComponent<Projectile>();

        projectile.setTarget(target.transform);
        
        //Debug.Log("Act");
    }

    

    protected void OnEnemyDeath(Enemy enemy)
    {
        //_targets.Remove(enemy);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();

        //Ignore collisions with non-enemies
        if (eb == null)
            return;

        //If we are not already targeting enemy
        if (_targets.Contains(eb) == false)
        {
            _targets.Add(eb);
            eb.OnEnemyDied += HandleEnemyDeath;
        }
        else
        {
            //Enemy is already being targeted;
            return;
        }

        PrioritizeTargets();

        if (CurrentState == TowerState.Idle)
            TransitionToState(TowerState.Acting);

        //Debug.Log("Added Enemy to targets");
    }

    /// <summary>
    /// Handles EnemyDied events by removing them from target list
    /// </summary>
    /// <param name="enemy">enemy that just died</param>
    void HandleEnemyDeath(Enemy enemy)
    {
        _targets.Remove(enemy);
        enemy.OnEnemyDied -= HandleEnemyDeath;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();
        if (eb == null)
            return;

        _targets.Remove(eb);
        Debug.Log("Removed Enemy from targets");
    }
    
}

