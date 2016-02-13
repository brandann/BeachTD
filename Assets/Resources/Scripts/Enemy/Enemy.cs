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

    public GameObject mBurstPrefab;

    public Sprite HealthSprite75;
    public Sprite HealthSprite50;
    public Sprite HealthSprite25;
    private Renderer spriteRenderer;
    private float _redColorTime;
    private bool _redColorActive;
    private Color _lastColor = Color.white;

    public CameraShake mCameraShake;
	
	public bool HasEgg
	{
        get { return _carriedEgg != null; }		
	}
	#endregion
	
	#region Private Members
    private Egg _carriedEgg;
    private GameObject _powPrefab;
    private GameObject _coinPrefab;
    private EnemyMovement _movement;
    private float ORG_HEALTH;
    private int mTargetedCount = 0;
	#endregion
	
	#region Unity
	void Awake () 
	{
        ORG_HEALTH = Health;
		CurrentEnemyState = EnemyState.Active;
		global = GameObject.Find("Global").GetComponent<Global>();
        _powPrefab = Resources.Load("Prefabs/temp-pow") as GameObject;
        _coinPrefab = Resources.Load("Prefabs/Coin") as GameObject;
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
        this.GetComponent<EnemyMovement>().SetEggSpeedMod();
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

        var go = GameObject.Instantiate(mBurstPrefab);
        go.transform.position = this.transform.position;

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
		//KillThisEnemy();
	}
	
	public void KillThisEnemy()
	{
        int eggValue = 5;
		if(HasEgg)
            DropCarriedEgg();	
			
		Instantiate(_powPrefab, this.transform.position, Quaternion.identity );

        if (this.gameObject.name.Contains("EnemyA"))
        {
            DropCoin(0, 0, eggValue);
        }
        else if (this.gameObject.name.Contains("EnemyB"))
        {
            DropCoin(.25f, 0, eggValue);
            DropCoin(-.25f, 0, eggValue);
        }
        else if (this.gameObject.name.Contains("EnemyC"))
        {
            DropCoin(-.25f, .25f, eggValue);
            DropCoin(.25f, .25f, eggValue);
            DropCoin(0, -.25f, eggValue);
        }
        
		if (ThisEnemyDied != null)
			ThisEnemyDied(this);
		
		if (SomeEnemyDied != null)
			SomeEnemyDied(this);
		
		global.enemyManager.Remove(this.gameObject);
	}

    private void DropCoin(float x, float y, int value)
    {
        Vector3 vec = new Vector3(x + this.transform.position.x, y + this.transform.position.y, 0);
        GameObject obj = Instantiate(_coinPrefab, vec, Quaternion.identity) as GameObject;
        obj.GetComponent<CoinBehavior>().setValue(value);
    }

    public void RemoveAfterReachedStart()
    {
        if (HasEgg)
            KillCarriedEgg();
        
        global.enemyManager.Remove(this.gameObject);

        if (mCameraShake == null)
        {
            mCameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        }

        mCameraShake.Shake();

    }

    public int getTargetedCount() 
    { 
        return this.mTargetedCount; 
    }

    public void deltaTargetedCount(int d)
    {
        if (this.mTargetedCount < 0) { this.mTargetedCount = 0; }
        bool show = (this.mTargetedCount == 0)? true:false;
        this.mTargetedCount += d;

        if(this.mTargetedCount == 0)
        {
            // decremented to 0, hide target
            this.gameObject.GetComponentInChildren<TargetBehavior>().hide();
        }
        else if(show)
        {
            // target count is above 0 and it started at 0, show target
            //this.gameObject.GetComponent<TargetBehavior>().show();
            this.gameObject.GetComponentInChildren<TargetBehavior>().show();
        }
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
