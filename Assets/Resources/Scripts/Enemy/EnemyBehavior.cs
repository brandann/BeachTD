using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	#region Unity

	void Awake () 
	{
		Speed = this.GetComponent<EnemyMovement>().speed;
	}
	
	void Update () 
	{
		
	}
	#endregion
	
	#region Public Members
	public bool Boss;
	public bool HasEgg = false;
	public float Health;
	public float Speed;
	public enum EnemyState { Active, Stunned, Dying }
	public EnemyState CurrentEnemyState;
	public SpriteRenderer NormalSpriteRenderer;
	public SpriteRenderer BossSpriteRenderer;
	#endregion
	
	#region Public Methods
	#endregion
}
