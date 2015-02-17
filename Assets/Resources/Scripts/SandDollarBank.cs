﻿using UnityEngine;
using System.Collections;

public class SandDollarBank : MonoBehaviour {

    #region Events
    public delegate void SandDollarsChanged(int dollars);
    public static event SandDollarsChanged OnSandDollarsChanged;

    #endregion

    public int StartingDollars = 100;

    public int SandDollars
    {
        get
        {
            return _sandDollars;
        }

        set
        {
            _sandDollars = value;
            if (OnSandDollarsChanged != null)
                OnSandDollarsChanged(_sandDollars);
        }
    }

    public void AddDollars(int dollars)
    {
        SandDollars += dollars;
    }

    public void SubtractDollars(int dollars)
    {
        if (dollars <= 0)
            Debug.LogWarning("Did you mean to sub: " + dollars);

        SandDollars -= dollars;
    }    
    

	// Use this for initialization
	void Start () {
        SandDollars = StartingDollars; //Todo Set this by the level logic
	
	}

    void OnEnable()
    {
        Enemy.SomeEnemyDied += AddMoneyUponEnemyDeath;
    }

    void OnDisable()
    {
        Enemy.SomeEnemyDied -= AddMoneyUponEnemyDeath;
    }

    private int _sandDollars;

    private void AddMoneyUponEnemyDeath(Enemy enemy)
    {
        Debug.Log("Add money: " + enemy.EnemyKillValue.ToString());
        SandDollars += enemy.EnemyKillValue;
    }
	
}