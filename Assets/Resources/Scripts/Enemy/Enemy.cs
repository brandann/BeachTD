using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied OnEnemyDied;

	#region Public Members
	public float Health;
	public enum EnemyState { Active, Stunned, Dying }
	public EnemyState CurrentEnemyState;
	public Global global;
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
	
	public void PickUpEgg(GameObject go = null)
	{
		// crab already has an egg
		if(HasEgg)
		{
			return;
		}
		
		// get an egg from the goal location
		if(go == null)
		{
			// if the egg manager has any eggs, get a egg
			if(global.eggManager.GetActiveCount() > 0)
			{
				HasEgg = true;
				global.eggManager.Remove();
			}
		}
		
		// if the egg exists
		else if(go != null)
		{
			global.eggManager.Remove(go);
			HasEgg = true;
		}
		
		return;
		/*
		if(go == null)
		{
			Egg = global.eggManager.GetEggFromGoal();
		}
		else
		{
			Egg = go;
		}
		
		if(Egg != null)
		{
			Egg.transform.localScale = new Vector3(.5f, .5f, .5f);
			Egg.transform.parent = transform.parent;
			Egg.transform.position = transform.position;
			Egg.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			Egg.GetComponent<CircleCollider2D>().enabled = false;
		}
		*/
	}
	
	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			Dead();
			//dead
			Destroy(this.gameObject);
			if (OnEnemyDied != null)
				OnEnemyDied(this);
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
			global.eggManager.EnemyKillEgg();
		}
	}
	#endregion
	
	#region Private Methods
	private void Dead()
	{
		//dead
		Debug.Log("Enemy Dead");
		
		if(HasEgg)
		{
			global.eggManager.DropEgg(this.transform.position);
		}
		GameObject prefab = Resources.Load("Prefabs/temp-pow") as GameObject;
		GameObject SpawnedPrefab = Instantiate(prefab) as GameObject;
		SpawnedPrefab.transform.position = this.transform.position;
		Destroy(this.gameObject);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "egg")
		{
			PickUpEgg(collision.gameObject);
		}
	}
	
	private void Pause(bool pause)
	{
		gameObject.SetActive(pause);
	}
	#endregion
}
