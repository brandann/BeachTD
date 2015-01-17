using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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
			//GameObject Prefab = this.transform.FindChild("egg").gameObject;
			//GameObject SpawnedPrefab = Instantiate(global.EggPrefab) as GameObject;
			//SpawnedPrefab.transform.position = this.transform.position;
			global.SpawnPrefab(global.EggPrefab, this.transform.position);
/*			egg.transform.parent = GameObject.Find("Global").transform;
			egg.transform.position = this.transform.position;
			egg.transform.localScale = new Vector3(1,1,1);*/
		}
		Destroy(this.gameObject);
	}
	
	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			Dead();
		}
	}
}
