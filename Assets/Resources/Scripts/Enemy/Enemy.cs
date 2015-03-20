using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    #region Events
    public delegate void EnemyDied(Enemy enemy);    

    //Use when you want to know if a particular enemy instance dies
    public event EnemyDied ThisEnemyDied;

    //Use if you want to know when any enemy object dies
    public static event EnemyDied SomeEnemyDied;

    #endregion

    #region Public Members
    public float Health;
	public enum EnemyState { Active, Stunned, Dying }
	
	public EnemyState CurrentEnemyState;
	public Global global;
    public int EnemyKillValue;

	
	
	public bool HasEgg
	{
        get { return _carriedEgg != null; }		
	}
	#endregion
	
	#region Private Members
    private Egg _carriedEgg;
	private Color _currentColor = Color.white;
    private GameObject _powPrefab;
    private EnemyMovement _movement;
	#endregion
	
	#region Unity
	void Awake () 
	{
		CurrentEnemyState = EnemyState.Active;
		global = GameObject.Find("Global").GetComponent<Global>();
        _powPrefab = Resources.Load("Prefabs/temp-pow") as GameObject;
        _movement = gameObject.GetComponent<EnemyMovement>();
	}
	
	void Update () 
	{
		if(CurrentEnemyState != EnemyState.Active)
		{
			Debug.Log("Enemy Not Active");
			this.GetComponent<EnemyMovement>().enabled = false;
		}
		if(Health <= 0)
		{
			KillThisEnemy ();
		}
	}
	#endregion
	
	#region Public Methods

    private void PickupEgg(Egg egg)
    {
        egg.Grab(gameObject);
        _carriedEgg = egg;
        //this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void DropCarriedEgg()
    {
        _carriedEgg.Drop();
        _carriedEgg = null;
    }

    private void KillCarriedEgg()
    {
        _carriedEgg.Kill();
        _carriedEgg = null;
    }
	
	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			KillThisEnemy ();			
		}
	}
	
	public void OnTouchDown()
	{
		KillThisEnemy();
	}
	
	public void KillThisEnemy()
	{		
		Debug.Log("Enemy Dead");
		if(HasEgg)
            DropCarriedEgg();	
			
		Instantiate(_powPrefab, this.transform.position, Quaternion.identity );	
		
		if (ThisEnemyDied != null)
			ThisEnemyDied(this);
		
		if (SomeEnemyDied != null)
			SomeEnemyDied(this);
		
		global.enemyManager.Remove(this.gameObject);
	}

    public void RemoveAfterReachedStart()
    {
        if (HasEgg)
            KillCarriedEgg();
        
        global.enemyManager.Remove(this.gameObject);

    }
	#endregion
	
	#region Private Methods


    private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "egg")
		{
            if (HasEgg)
                return;

            Egg egg = col.gameObject.GetComponent<Egg>();
            if (egg == null)
            {
                Debug.LogError("Shouldn't be null");
                return;
            }

            PickupEgg(egg);
            _movement.ReverseDirection();
        }         
		
	}
	
	private void Pause(bool pause)
	{
		gameObject.SetActive(pause);
	}
	#endregion
}
