using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	#region Public Methods
	
	public bool SpawnEnemy(string e, Vector3 s)
	{
		string strEnemy = "Prefabs/" + e;
		Debug.Log(strEnemy);
		GameObject SpawnedPrefab = Resources.Load(strEnemy) as GameObject;
		GameObject SpawnedEnemy = Instantiate(SpawnedPrefab) as GameObject;
		SpawnedEnemy.transform.position = s;
		return false;
	}
	
	#endregion

	#region Unity

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	#endregion
}
