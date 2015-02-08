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
	#endregion
	
	#region Private Members
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
		get{return Egg != null;}
	}
	
	public void updateWaypoints(Vector3[] waypoints)
	{
	
	}
	
	public void PickUpEgg(GameObject go = null)
	{
		if(go == null)
		{
			Egg = global.GetEggFromGoal();
		}
		else
		{
			Egg = go;
		}
		
		if(Egg != null)
		{
			Egg.transform.localScale = new Vector3(.5f, .5f, .5f);
			Egg.transform.parent = transform;
			Egg.transform.position = transform.position;
			Egg.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			Egg.GetComponent<CircleCollider2D>().enabled = false;
		}
	}
	
	private void Dead()
	{
		//dead
		Debug.Log("Enemy Dead");
		
		if(HasEgg)
		{
			global.DropEgg(this.transform.position);
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
			if(!HasEgg)
			{
				//this.gameObject.GetComponent<EnemyMovement>().ReverseDirection();
				PickUpEgg(collision.gameObject);
			}
			
		}
	}
}
