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
	
	private void Dead()
	{
		//dead
		Debug.Log("Enemy Dead");
		
		if(_hasegg)
		{
			global.SpawnPrefab(global.EggPrefab, this.transform.position);
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
}
