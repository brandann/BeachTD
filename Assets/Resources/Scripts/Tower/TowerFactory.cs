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
    public GameObject PathPrefab;

    public static TowerFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TowerFactory>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Recycles the tower.
    /// </summary>
    /// <param name="tower">Tower type to be returned to the factory</param>
    public void RecycleTower(Tower tower)
    {
        if (tower == null)
        {
            Debug.LogError("Invalid enemy parameter");
            return;
        }   

        tower.Initialize();

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
        if (tower == null)
        {
            Debug.LogError("Can't create null tower");
            return null;
        }

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

        towerToReturn.gameObject.SetActive(true);        
		TowersDispensed++;
        _deployedTowers.Add(towerToReturn);
        return towerToReturn;
    }

    public GameObject CreatePathSquare()
    {
        return Instantiate(PathPrefab);
    }
    
    void Awake()
    {
        InitializeFactory();
        Debug.Log("initialize factory");
    }

    //The towerfactory singleton
    private static TowerFactory _instance;

    //Cached towers ready to be deployed
    private static Queue<GameObject>[] _Pool;

    //# no reason use profiler to experiment if needed
    private static int _PoolSize = 15;
    
    //Used as indexes for the pool     
    private static Dictionary<System.Type,int> _TypeIndex;    
   
    private List<GameObject> _Prefabs;

    private List<Tower> _deployedTowers;


    /// <summary>
    /// Fill enemy pools with new enemies.
    /// </summary>
    public void InitializeFactory()
    {
        _Prefabs = new List<GameObject>(3);
        _Prefabs.Add( MeleePrefab);
        _Prefabs.Add( RangedPrefab );
        _Prefabs.Add( SlowPrefab );

        _deployedTowers = new List<Tower>();               
    
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
                DontDestroyOnLoad(tmp); 
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

    void OnLevelWasLoaded(int lvl)
    {
        RecycleAllDeployed();
    }

    private void RecycleAllDeployed()
    {
        foreach (Tower t in _deployedTowers)
            RecycleTower(t);

        _deployedTowers.Clear();
    }
    

}