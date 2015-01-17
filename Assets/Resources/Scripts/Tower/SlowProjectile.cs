using UnityEngine;
using System.Collections;

/// <summary>
/// Slow projectile is designed to work with a "helper" child game object. The child holds the sprite and colliders and passes
/// collisions up. 
/// </summary>
public class SlowProjectile : Projectile {

    public float Duration = 0.5f;

    public void Start()
    {
        Damage = 0.32f;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit");
        EnemyMovement en = other.gameObject.GetComponent<EnemyMovement>();

        //Ignore non-enemies and non-targets
        if (en == null || other.transform != Target)
            return;

        en.UpdateSpeedMod(EnemyMovement.EnemyMovementSpeed.Slow, Duration);

        Destroy(gameObject);        
        
    }
}
