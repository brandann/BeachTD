using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    #region Events
    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied ThisEnemyDied;

    public static event EnemyDied SomeEnemyDied;

    #endregion

    #region Public Members
    public float Health;
	public enum EnemyState { Active, Stunned, Dying }
	public EnemyState CurrentEnemyState;
	public Global global;
    public int EnemyKillValue;
	private GameObject Egg;
	
	public bool HasEgg
	{
		get
		{
			return _hasEgg;
		}
		set
		{
			_hasEgg = value;
			if(_hasEgg)
			{
				this.renderer.material.color = Color.red;
			}
			else
			{
				this.renderer.material.color = Color.white;
			}
			
			_currentColor = this.renderer.material.color;
		}
	}
	#endregion
	
	#region Private Members
	private bool _hasEgg = false;
	private Color _currentColor = Color.white;
	#endregion
	
	#region Unity
	void Awake () 
	{
		CurrentEnemyState = EnemyState.Active;
		global = GameObject.Find("Global").GetComponent<Global>();
	}
	
	void Update () 
	{
		if(CurrentEnemyState != EnemyState.Active)
		{
			this.GetComponent<EnemyMovement>().enabled = false;
		}
		if(Health <= 0)
		{
			Dead();
		}
	}
	#endregion
	
	#region Public Methods
	public void updateWaypoints(Vector3[] waypoints)
	{
		
	}
	
	public void ApplyEggToCrab(EggManager.EggLocations el)
	{
		// crab already has an egg
		if(HasEgg)
		{
			Debug.Log("crab has egg already");
			return;
		}
		
		if(el == EggManager.EggLocations.Enemy)
		{
			Debug.LogError("Invalid location for crab to recieve egg");
			return;
		}
		else if(el == EggManager.EggLocations.End)
		{
			// if the egg manager has any eggs, get a egg
			if(global.eggManager.GetActiveCount() > 0)
			{
				HasEgg = true;
				global.eggManager.TransferEgg(EggManager.EggLocations.End, EggManager.EggLocations.Enemy);
			}
		}
		else if(el == EggManager.EggLocations.Path)
		{
			HasEgg = true;
			global.eggManager.TransferEgg(EggManager.EggLocations.Path, EggManager.EggLocations.Enemy);
		}
	}
	
	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			Dead();
			//dead
			Destroy(this.gameObject);			
		}
	}
	
	public void OnTouchDown()
	{
		Dead();
	}
	
	public void ResetColor()
	{
		this.renderer.material.color = _currentColor;
	}
	
	public void AtGoal()
	{
		if(HasEgg)
		{
			global.eggManager.TransferEgg(EggManager.EggLocations.Enemy, EggManager.EggLocations.Start);
		}
		
		Kill();
	}
	#endregion
	
	#region Private Methods
	private void Dead()
	{
		if(HasEgg)
		{
			global.eggManager.DropEgg(this.transform.position);
		}
		
		Kill();
	}
	
	private void Kill()
	{
		GameObject prefab = Resources.Load("Prefabs/temp-pow") as GameObject;
		GameObject SpawnedPrefab = Instantiate(prefab) as GameObject;
		SpawnedPrefab.transform.position = this.transform.position;
		
		if (ThisEnemyDied != null)
			ThisEnemyDied(this);
		
		if (SomeEnemyDied != null)
			SomeEnemyDied(this);
		
		global.enemyManager.Remove(this.gameObject);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "egg")
		{
			if(!HasEgg)
			{
				ApplyEggToCrab(EggManager.EggLocations.Path);
				Destroy(collision.gameObject);
			}
		}
	}
	
	private void Pause(bool pause)
	{
		gameObject.SetActive(pause);
	}
	#endregion
}
