using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Determines the availabilty of upgrades based on tower selection and funds available etc.
/// Controls the presentation of the upgrade UI elements and handles the user selection thereof.
/// </summary>
public class TowerUpgradeManager : MonoBehaviour
{   

    #region InspectorAssigned
    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;
    public GameObject OpenAreaPrefab;

    

    //Maximum number of upgrades available to each type of tower
    

    public int MeleeTowerCost;
    public int RangedTowerCost;
    public int SlowTowerCost;

    public int UpgradeCost;

    public float SpeedUpgradePercentage;
    public float DamageUpgradePercentage;
    //public int RangeUpgradeCost = -1;
    //public int DamageUpgradeCost = -1;
    //public int SpecialUpgradeCost = -1;
    #endregion

    private int MaxMeleeRange = 2;
    private int MaxMeleeSpeed = 2;
    private int MaxMeleeDamage = 2;
    private int MaxMeleeSpecial = 2;

    private int MaxRangedRange = 2;
    private int MaxRangedSpeed = 2;
    private int MaxRangedDamage = 2;
    private int MaxRangedSpecial = 2;

    private int MaxSlowRange = 2;
    private int MaxSlowSpeed = 2;
    private int MaxSlowDamage = 2;
    private int MaxSlowSpecial = 2;     

    

    #region MonoBehaviour
    void Awake()
    {
        _specialUpgrade = new Tower.Upgrade(0, 0, 0, 1);
        _10percentSpeed = new Tower.Upgrade(0, SpeedUpgradePercentage);
        _10percentDamage = new Tower.Upgrade(0, 0, DamageUpgradePercentage);
        _10percentRange = new Tower.Upgrade(0.3f);

        
        FillLookups();       

        if (MeleeTowerCost < 0 || RangedTowerCost < 0 || SlowTowerCost < 0)
            Debug.LogWarning("Forgot to set build costs in upgrade manager?");

        //if (RangeUpgradeCost < 0 || UpgradeCost < 0 || DamageUpgradeCost < 0 || SpecialUpgradeCost < 0)
        //    Debug.LogWarning("Forgot to set upgrade costs in upgrade manager?");

        if (UpgradeCost < 0)
            Debug.LogWarning("Forgot to set upgrade costs in upgrade manager?");

        
    }

    void Start()
    {
        _bank = GameObject.Find("Global").GetComponent<SandDollarBank>();
        if (_bank == null)
            Debug.LogError("Can't find bank");
    }



    #endregion

    #region Buttons

    public void BuildMeleeFromArea(OpenAreaBehavior openArea)
    {
        BuildTower(MeleePrefab.GetComponent<MeleeTower>(), openArea);
        _bank.SubtractDollars(MeleeTowerCost);
    }

    public void BuildRangedFromArea(OpenAreaBehavior openArea)
    {
        BuildTower(RangedPrefab.GetComponent<RangedTower>(), openArea);
        _bank.SubtractDollars(RangedTowerCost);
    }

    public void BuildSlowFromArea(OpenAreaBehavior openArea)
    {
        BuildTower(SlowPrefab.GetComponent<SlowTower>(), openArea);
        _bank.SubtractDollars(SlowTowerCost);
    }

    public void UpgradeTower(Tower tower)
    {
        UpgradeTower(tower, _10percentSpeed);
        UpgradeTower(tower, _10percentRange);
        _bank.SubtractDollars(UpgradeCost);
    }

    private void UpgradeTower(Tower tower, Tower.Upgrade up)
    {
        

        if (tower == null)
        {
            Debug.LogWarning("missing tower");
            return;
        }

        tower.UpgradeTower(up);
    }

    public void SellTower(Tower touchedTower)
    {
        

        Instantiate(OpenAreaPrefab, touchedTower.transform.position, Quaternion.identity);
        TowerFactory.Instance.RecycleTower(touchedTower);        
        _bank.AddDollars(touchedTower.Cost);
    }

    #endregion    
    
    
    private Tower.Upgrade _specialUpgrade;
    private Tower.Upgrade _10percentSpeed;
    private Tower.Upgrade _10percentDamage;
    private Tower.Upgrade _10percentRange;

    private Dictionary<Tower, int> _towerLookup;

    private int[,] _maxUpgrades;
    
    private readonly int RANGEINDEX = 0;
    private readonly int SPEEDINDEX = 1;
    private readonly int DAMAGEINDEX = 2;
    private readonly int SPECIALINDEX = 3;
    
    
    
    private SandDollarBank _bank;
    
    /// <summary>
    /// Makes it a bit easier to look up the max upgrade level of tower by attribute
    /// </summary>
    private void FillLookups()
    {
        _towerLookup = new Dictionary<Tower, int>();

        _towerLookup.Add(MeleePrefab.GetComponent<Tower>(), 0);
        _towerLookup.Add(RangedPrefab.GetComponent<Tower>(), 1);
        _towerLookup.Add(SlowPrefab.GetComponent<Tower>(), 2);

        _maxUpgrades = new int[3, 4];
        _maxUpgrades[0, RANGEINDEX] = MaxMeleeRange;
        _maxUpgrades[0, SPEEDINDEX] = MaxMeleeSpeed;
        _maxUpgrades[0, DAMAGEINDEX] = MaxMeleeDamage;
        _maxUpgrades[0, SPECIALINDEX] = MaxMeleeSpecial;

        _maxUpgrades[1, RANGEINDEX] = MaxRangedRange;
        _maxUpgrades[1, SPEEDINDEX] = MaxRangedSpeed;
        _maxUpgrades[1, DAMAGEINDEX] = MaxRangedDamage;
        _maxUpgrades[1, SPECIALINDEX] = MaxRangedSpecial;

        _maxUpgrades[2, RANGEINDEX] = MaxSlowRange;
        _maxUpgrades[2, SPEEDINDEX] = MaxSlowSpeed;
        _maxUpgrades[2, DAMAGEINDEX] = MaxSlowDamage;
        _maxUpgrades[2, SPECIALINDEX] = MaxSlowSpecial;
    }   

    

    private void BuildTower(Tower t, OpenAreaBehavior touchedArea)
    {
        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(t);
        tower.transform.position = touchedArea.transform.position;

        //Out with the old
        Destroy(touchedArea.gameObject);
        touchedArea = null;

   
        
        // call enemy manager and tell her a tower has been built
        Global go = GameObject.Find("Global").GetComponent<Global>();
        go.enemyManager.NotifyTowerBuilt();
        GameObject.Find("Seagull").GetComponent<Seagull>().StartPlane();
    }
    
    public  bool CanBuildMelee()
    {
        return _bank.SandDollars >= MeleeTowerCost;
    }

    public bool CanBuildRanged()
    {
        return _bank.SandDollars >= RangedTowerCost;
    }

    public bool CanBuildSlow()
    {
        return _bank.SandDollars >= SlowTowerCost;
    }


    /// <summary>
    /// Determines if _touched tower's speed attribute can be upgraded further
    /// </summary>
    /// <returns></returns>
    public bool CanUpgradeSpeed(Tower tower)
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(tower, out index);
        
        max = _maxUpgrades[index, SPEEDINDEX];


        return (tower.SpeedUpgrades < max) && (UpgradeCost <= _bank.SandDollars);        

    }

    public bool CanUpgradeRange(Tower tower)
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(tower, out index);

      
        max = _maxUpgrades[index, RANGEINDEX];

        return false; // return (_touchedTower.RangeUpgrades < max) && (RangeUpgradeCost <= _bank.SandDollars);
    }

    public bool CanUpgradeDamage(Tower tower)
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(tower, out index);

        
        max = _maxUpgrades[index, DAMAGEINDEX];

        return false; // return (_touchedTower.DamageUpgrades < max) && (DamageUpgradeCost <= _bank.SandDollars);    
    }

    /// <summary>
    /// Hook for special abilities upgrade
    /// </summary>
    /// <param name="tower"></param>
    /// <returns></returns>
    public bool CanUpgradeSpecial(Tower tower)
    {
        return false;   
    }

    public bool CanSellTower(Tower tower)
    {
        return true; 
    }   
    
	
}
