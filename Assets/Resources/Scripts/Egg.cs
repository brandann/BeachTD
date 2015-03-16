using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

    public static event EggStatusChanged EggPickedUp;
    public static event EggStatusChanged EggDropped;
    public static event EggStatusChanged EggKilled;

    public delegate void EggStatusChanged(Egg egg);
    
    private CircleCollider2D _collider;
    private GameObject _toFollow;
    private float _smallRadius = 0.25f;
    private float _largeRadius = 0.32f;

    public bool BeingCarried { get { return _toFollow != null; } }

    void Start()
    {
        _collider = gameObject.GetComponent<CircleCollider2D>();
        _toFollow = null;

        //Bigger radius makes more likely that enemies will collide when they reach the end of path
        _collider.radius = _largeRadius;
    }

    void Update()
    {
        if(_toFollow != null)
            transform.position = _toFollow.transform.position;
    }

    public void Grab(GameObject grabedBy)
    {
        _toFollow = grabedBy;
        _collider.enabled = false;

        if (EggPickedUp != null)
            EggPickedUp(this);
    }

    public void Drop()
    {
        _toFollow = null;
        _collider.enabled = true;

        //Egg must have been moved from starting location so shrink collider down to fit the sprite render
        _collider.radius = _smallRadius;

        if (EggDropped != null)
            EggDropped(this);
    }

    public void Kill()
    {
        if (EggKilled != null)
            EggKilled(this);

    }
}
