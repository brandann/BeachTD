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
    public enum ColorState {Slow, Normal, Damage}
	public enum EnemyState { Active, Stunned, Dying }
	
	public EnemyState CurrentEnemyState;
	public Global global;
    public int EnemyKillValue;

    public Sprite HealthSprite75;
    public Sprite HealthSprite50;
    public Sprite HealthSprite25;
    private Renderer spriteRenderer;
    private float _redColorTime;
    private bool _redColorActive;
    private Color _lastColor = Color.white;
	
	public bool HasEgg
	{
        get { return _carriedEgg != null; }		
	}
	#endregion
	
	#region Private Members
    private Egg _carriedEgg;
    private GameObject _powPrefab;
    private EnemyMovement _movement;
    private float ORG_HEALTH;
	#endregion
	
	#region Unity
	void Awake () 
	{
        ORG_HEALTH = Health;
		CurrentEnemyState = EnemyState.Active;
		global = GameObject.Find("Global").GetComponent<Global>();
        _powPrefab = Resources.Load("Prefabs/temp-pow") as GameObject;
        _movement = gameObject.GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<Renderer>();
	}
	
	void Update () 
	{
        if (_redColorActive && Time.timeSinceLevelLoad - _redColorTime > .05f)
        {
            spriteRenderer.material.color = _lastColor;
        }
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
	
    
    public void ChangeColor(ColorState cs)
    {
        switch(cs)
        {
            case(ColorState.Damage):
                spriteRenderer.material.color = Color.red;
                _redColorActive = true;
                _redColorTime = Time.timeSinceLevelLoad;
                break;
            case(ColorState.Normal):
                spriteRenderer.material.color = Color.white;
                _lastColor = Color.white;
                break;
            case(ColorState.Slow):
                spriteRenderer.material.color = Color.blue;
                _lastColor = Color.blue;
                break;
        }
    }

	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
        ChangeColor(ColorState.Damage);

        if (Health <= (ORG_HEALTH * .75f) && Health > (ORG_HEALTH * .5f))
        {
            this.GetComponent<SpriteRenderer>().sprite = HealthSprite75;
        }
        else if (Health <= (ORG_HEALTH * .5f) && Health > (ORG_HEALTH * .25f))
        {
            this.GetComponent<SpriteRenderer>().sprite = HealthSprite50;
        }
        else if (Health <= (ORG_HEALTH * .25f) && Health > (ORG_HEALTH * 0))
        {
            this.GetComponent<SpriteRenderer>().sprite = HealthSprite25;
        }

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
		//Debug.Log("Enemy Dead");
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
