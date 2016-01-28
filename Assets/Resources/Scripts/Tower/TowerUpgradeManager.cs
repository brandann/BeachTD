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
    //Prefabs to build
    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;
    public GameObject OpenAreaPrefab;
        
    public int MaximumNumberOfUpgrades;    
        
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

    #region MonoBehaviour
    void Awake()
    {        
        FillLookups();     

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
        UpgradeTower(tower, new Tower.Upgrade(0,SpeedUpgradePercentage,DamageUpgradePercentage) );       
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

        int index = -1;
        
        _bank.AddDollars(_towerCosts[index]);
    }

    #endregion    
        
    private Tower.Upgrade _specialUpgrade;
    private Tower.Upgrade _10percentSpeed;
    private Tower.Upgrade _10percentDamage;
    private Tower.Upgrade _10percentRange;    

    private int[] _towerCosts;      
    
    
    private SandDollarBank _bank;
    
    /// <summary>
    /// Makes it a bit easier to look up the max upgrade level of tower by attribute
    /// </summary>
    private void FillLookups()
    {
        _towerCosts = new int[3];
        _towerCosts[0] = MeleeTowerCost;
        _towerCosts[1] = RangedTowerCost;
        _towerCosts[2] = SlowTowerCost;        
    }       

    private void BuildTower(Tower t, OpenAreaBehavior touchedArea)
    {
        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(t);
        //tower.transform.position = touchedArea.transform.position;

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
    public bool CanUpgrade(Tower tower)
    {
        //May as well use speed upgrades since we're always upgrading speed and damage now no reason to check both
        return (tower.NumSpeedUpgradesApplied < MaximumNumberOfUpgrades) && (UpgradeCost <= _bank.SandDollars);
    }


    public bool CanSellTower(Tower tower)
    {
        return true; 
    }   
    
	
}
