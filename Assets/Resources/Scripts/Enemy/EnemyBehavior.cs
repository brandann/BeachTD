using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	#region Unity

	void Awake () 
	{
		CurrentEnemyState = EnemyState.Active;
	}
	
	void Update () 
	{
		
	}
	#endregion
	
	#region Public Members
	public bool HasEgg = false;
	public float Health;
	public enum EnemyState { Active, Stunned, Dying }
	public EnemyState CurrentEnemyState;
	public SpriteRenderer NormalSpriteRenderer;
	public SpriteRenderer BossSpriteRenderer;
	#endregion
	
	#region Public Methods
	#endregion
}
