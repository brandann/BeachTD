using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerFactory : MonoBehaviour
{
	public static int TowersDispensed{ get; protected set;}

    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;

    public static TowerFactory Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = GameObject.FindObjectOfType<TowerFactory>();
            }
            return mInstance;
        }
    }

    /// <summary>
    /// Recycles the tower.
    /// </summary>
    /// <param name="enemy">Tower type to be returned to the factory</param>
    public void RecycleEnemy(Tower tower)
    {
        if (tower == null)
        {
            Debug.LogError("Invalid enemy parameter");
            return;
        }        

        //tower.Initialize();

        int eIndex = -1;
        _TypeIndex.TryGetValue(tower.GetType(), out eIndex);
        _Pool [eIndex].Enqueue(tower.gameObject);
    }
    

    /// <summary>
    /// Returns an Tower of the type passed.
    /// </summary>
    /// <param name="type">Type of Tower to create</param>
    /// <returns>Initialized tower of the type passed in</returns>
    public Tower CreateTower(Tower tower)
    {
        int towerIndex;
        _TypeIndex.TryGetValue(tower.GetType(), out towerIndex);

        Tower towerToReturn = null;

        if (towerIndex < 0 || towerIndex >= _Prefabs.Count)
        {
            Debug.LogWarning( "Invalid Tower request: " );
            return null;
        }

        if (_Pool [towerIndex].Count == 0)      //Pool is empty spawn a new enemy from _Prefabs
        {
            towerToReturn = (GameObject.Instantiate(_Prefabs[towerIndex]) as GameObject).GetComponent<Tower>();
            //TODO Add new tower to pool and increment _poolsize
            

        } else                                 //Return next enemy
        {
            GameObject go = _Pool [towerIndex].Dequeue();
            if (go == null)
                Debug.LogWarning("Go = null _Pool[" + towerIndex + "].count: " + _Pool [towerIndex].Count);
            else
            {
                towerToReturn = go.GetComponent<Tower>();
            }
        }

        towerToReturn.renderer.enabled = true;
        towerToReturn.collider2D.enabled = true;
		TowersDispensed++;
        return towerToReturn;
    }



    private static TowerFactory mInstance;
    
    private static Queue<GameObject>[] _Pool;     
    private static int _PoolSize = 15;             //# no reason use profiler to experiment if needed
    private static Dictionary<System.Type,int> _TypeIndex;    
    private static float mHighestEnemyFrequency = -1f;
    private static List<int> mAvailableEnemyIndicies;
    private List<GameObject> _Prefabs;
       

    //public 

    /// <summary>
    /// Fill enemy pools with new enemies.
    /// </summary>
    public void InitializeFactory()
    {
        _Prefabs = new List<GameObject>(3);
        _Prefabs[0] = MeleePrefab;
        _Prefabs[1] = RangedPrefab;
        _Prefabs[2] = SlowPrefab;

        mAvailableEnemyIndicies = new List<int>();
    
        _TypeIndex = new Dictionary<Type, int>();
        
        _Pool = new Queue<GameObject>[_Prefabs.Count];

        for (int i = 0; i < _Prefabs.Count; ++i)
        {
            _Pool[i] = new Queue<GameObject>(_PoolSize);

            for (int j = 0; j < _PoolSize; ++j)
            {
                GameObject tmp = GameObject.Instantiate(_Prefabs[i]) as GameObject;
                if (tmp == null)
                    Debug.LogError("Shouldn't insert null into queue");

                _Pool[i].Enqueue(tmp);
            }

        }

        //Fill out index for types of towers
        for (int i = 0; i < _Prefabs.Count; ++i)
        {
            _TypeIndex.Add(_Pool[i].Peek().GetComponent<Tower>().GetType(), i);
        }
            
        //Checking for a full pool
        for (int i = 0; i < _Prefabs.Count; ++i)
            if (_Pool [i].Count == 0)
                Debug.LogError("Missing i: " + i);

    }
    

}