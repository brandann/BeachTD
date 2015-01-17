using UnityEngine;
using System.Collections;

public class SlowHelper : MonoBehaviour
{
    private SlowProjectile _parentSlow;

    void Awake()
    {
        _parentSlow = gameObject.GetComponentInParent<SlowProjectile>();
    }

    /// <summary>
    /// Pases colliders up to slow projectile in parent for processing
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        _parentSlow.OnTriggerEnter2D(other);       
    }
}
