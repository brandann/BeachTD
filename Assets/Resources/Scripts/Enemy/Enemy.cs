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
	#endregion
	
	#region Private Members
	private bool _hasegg = false;
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
	
	public bool HasEgg
	{
		get{return _hasegg;}
		set{_hasegg = value;}
	}
	
	public void updateWaypoints(Vector3[] waypoints)
	{
	
	}
	
	public void PickUpEgg(GameObject go = null)
	{
		GameObject egg = null;
		if(go == null)
		{
			egg = global.GetEggFromGoal();
		}
		else
		{
			egg = go;
		}
		
		if(egg != null)
		{
			egg.transform.localScale = new Vector3(.5f, .5f, .5f);
			egg.transform.parent = transform;
			egg.transform.position = transform.position;
			egg.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			HasEgg = true;
			egg.GetComponent<CircleCollider2D>().enabled = false;
		}
	}
	
	private void Dead()
	{
		//dead
		Debug.Log("Enemy Dead");
		
		if(_hasegg)
		{
			global.DestroyEgg();
		}
		GameObject prefab = Resources.Load("Prefabs/temp-pow") as GameObject;
		GameObject SpawnedPrefab = Instantiate(prefab) as GameObject;
		SpawnedPrefab.transform.position = this.transform.position;
		Destroy(this.gameObject);
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
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "egg")
		{
			if(HasEgg)
			{
				return;
			}
			Debug.Log("here");
			//this.gameObject.GetComponent<EnemyMovement>().ReverseDirection();
			PickUpEgg(collision.gameObject);
		}
	}
}
