using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		EnemySpawner es = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		es.SpawnEnemy("Enemy", Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
