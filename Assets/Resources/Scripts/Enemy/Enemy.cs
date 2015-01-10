using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied OnEnemyDied;

	#region Public Members
	public bool HasEgg = false;
	public float Health;
	public enum EnemyState { Active, Stunned, Dying }
	public EnemyState CurrentEnemyState;
	#endregion
	
	#region Unity
	void Awake () 
	{
		CurrentEnemyState = EnemyState.Active;
	}
	
	void Update () 
	{
		if(CurrentEnemyState != EnemyState.Active)
		{
			this.GetComponent<EnemyMovement>().enabled = false;
		}
		if(Health <= 0)
		{
			//dead
			Debug.Log("Enemy Dead");
			Destroy(this.gameObject);
		}
	}
	#endregion
	
	public void updateWaypoints(Vector3[] waypoints)
	{
	
	}
	
	public virtual void TakeDamage(float damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			//dead
			Debug.Log("Enemy Dead");
			Destroy(this.gameObject);
            if (OnEnemyDied != null)
                OnEnemyDied(this);

		}
	}
}
