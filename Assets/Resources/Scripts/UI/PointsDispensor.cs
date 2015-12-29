using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PointsDispensor : MonoBehaviour {

    public GameObject PointsPrefab;

    private Queue<GameObject> _points;
    private int poolDepth = 10;

	// Use this for initialization
	void Start () {        

        _points = new Queue<GameObject>(poolDepth);

        //Fill object pool
        for (int i = 0; i < poolDepth; ++i)
        {
            GameObject points = Instantiate(PointsPrefab) as GameObject;
            points.SetActive(false);
            _points.Enqueue(points);
            DontDestroyOnLoad(points);
        }        
	
	}

    void OnEnable()
    {
        Enemy.SomeEnemyDied += HandleEnemyDeath;
        Seagull.OnGullKilled += HandleGullDeath;
    }

    void OnDisable()
    {
        Enemy.SomeEnemyDied -= HandleEnemyDeath;
        Seagull.OnGullKilled -= HandleGullDeath;
    }
	
	private void HandleEnemyDeath(Enemy enemy)
    {
        //GameObject points = _points.Dequeue();
        //points.SetActive(true);
        //points.transform.position = enemy.transform.position;
        //points.GetComponentInChildren<Text>().text = "+" + enemy.EnemyKillValue.ToString();
        //_points.Enqueue(points);

    }

    private void HandleGullDeath(Seagull gull)
    {
        GameObject points = _points.Dequeue();
        points.SetActive(true);
        points.transform.position = gull.transform.position;
        points.GetComponentInChildren<Text>().text = "+" + gull.GullKillValue;
        _points.Enqueue(points);
    }
}
