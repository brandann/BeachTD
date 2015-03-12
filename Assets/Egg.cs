using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

    public static event EggStatusChanged EggPickedUp;
    public static event EggStatusChanged EggDropped;
    public static event EggStatusChanged EggKilled;

    public delegate void EggStatusChanged(Egg egg);
    
    private Collider2D _collider;
    private GameObject _toFollow;

    public bool BeingCarried { get { return _toFollow != null; } }

    void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
        _toFollow = null;
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

        if (EggDropped != null)
            EggDropped(this);
    }

    public void Kill()
    {
        if (EggKilled != null)
            EggKilled(this);

    }
}
