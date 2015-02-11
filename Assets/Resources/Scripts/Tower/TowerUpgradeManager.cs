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
    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;
    public GameObject OpenAreaPrefab;

    public Button BuildMeleeButton;
    public Button BuildRangedButton;
    public Button BuildSlowButton;
    public Button SpeedUpButton;
    public Button RangeUpButton;
    public Button DamageUpButton;
    public Button SpecialUpButton;
    public Button SellButton;


    //Maximum number of upgrades available to each type of tower
    public int MaxMeleeRange = 2;
    public int MaxMeleeSpeed = 2;
    public int MaxMeleeDamage = 2;
    public int MaxMeleeSpecial = 2;

    public int MaxRangedRange = 2;
    public int MaxRangedSpeed = 2;
    public int MaxRangedDamage = 2;
    public int MaxRangedSpecial = 2;

    public int MaxSlowRange = 2;
    public int MaxSlowSpeed = 2;
    public int MaxSlowDamage = 2;
    public int MaxSlowSpecial = 2;

    public bool MenuActive { get; private set; }
    

    #region MonoBehaviour
    void Awake()
    {
        //Assign buttons
        _buttons = new Button[8];
        _buttons[0] = BuildMeleeButton;
        _buttons[1] = BuildRangedButton;
        _buttons[2] = BuildSlowButton;
        _buttons[3] = SpeedUpButton;
        _buttons[4] = RangeUpButton;
        _buttons[5] = DamageUpButton;
        _buttons[6] = SpecialUpButton;
        _buttons[7] = SellButton;

        //Ensure all buttons have been added in the inspector
        foreach (Button b in _buttons)
            if (b.gameObject == null)
                Debug.LogError("Missing button");

        FillLookups();


        //with no params actually hides all buttons
        ShowBuildButtons();
        ShowUpgradeButtons();
    }

    void OnEnable()
    {
        Tower.onTowerTouched += TowerTouched;
        OpenAreaBehavior.onAreaTouched += OpenAreaTouched;
    }

    void OnDisable()
    {
        Tower.onTowerTouched -= TowerTouched;
        OpenAreaBehavior.onAreaTouched -= OpenAreaTouched;
    }

    #endregion

    #region Buttons

    public void BuildMelee()
    {
        BuildTower(MeleePrefab.GetComponent<MeleeTower>());
    }

    public void BuildRanged()
    {
        BuildTower(RangedPrefab.GetComponent<RangedTower>());
    }

    public void BuildSlow()
    {
        BuildTower(SlowPrefab.GetComponent<SlowTower>());
    }

    public void UpgradeSpeed()
    {
        UpgradeTower(_10percentSpeed);
    }

    public void UpgradeRange()
    {
        UpgradeTower(_10percentRange);
    }

    public void UpgradeDamage()
    {
        UpgradeTower(_10percentDamage);
    }

    public void UpgradeSpecial()
    {
        UpgradeTower(_specialUpgrade);
    }

    public void SellTower()
    {
        if (_touchedTower == null)
        {
            Debug.LogWarning("Missing tower");
            return;
        }

        Instantiate(OpenAreaPrefab, _touchedTower.transform.position, Quaternion.identity);
        TowerFactory.Instance.RecycleTower(_touchedTower);
        ShowUpgradeButtons();
    }

    #endregion

    //Last tower that fired touch event
    private Tower _touchedTower;

    //Last open area that fired touch event
    private OpenAreaBehavior _touchedArea;

    private readonly Tower.Upgrade _specialUpgrade = new Tower.Upgrade(0, 0, 0, 1);
    private readonly Tower.Upgrade _10percentSpeed = new Tower.Upgrade(0, .1f);
    private readonly Tower.Upgrade _10percentDamage = new Tower.Upgrade(0, 0, 0.1f);
    private readonly Tower.Upgrade _10percentRange = new Tower.Upgrade(0.1f);

    private Dictionary<Tower, int> _towerLookup;

    private int[,] _maxUpgrades;
    private readonly int RANGEINDEX = 0;
    private readonly int SPEEDINDEX = 1;
    private readonly int DAMAGEINDEX = 2;
    private readonly int SPECIALINDEX = 3;
    private Button[] _buttons;

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

    private void UpgradeTower(Tower.Upgrade up)
    {
        //Clear UI 
        ShowUpgradeButtons();

        if (_touchedTower == null)
        {
            Debug.LogWarning("missing tower");
            return;
        }

        _touchedTower.UpgradeTower(up);
    }

    private void BuildTower(Tower t)
    {
        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(t);
        tower.transform.position = _touchedArea.transform.position;

        //Out with the old
        Destroy(_touchedArea.gameObject);
        _touchedArea = null;

        //Clear UI
        ShowBuildButtons();
    }

    private void TowerTouched(Tower tower)
    {
        //Debug.Log("Received touch in manager");

        //Ignore disabled towers
        if (tower.CurrentState == Tower.TowerState.Disabled)
            return;

        //Cache reference to tower 
        _touchedTower = tower;

        //Move to tower that was touched
        transform.position = tower.transform.position;

        bool showSpeed = CanUpgradeSpeed();
        bool showRange = CanUpgradeRange();
        bool showDamage = CanUpgradeDamage();
        bool showSpecial = CanUpgradeSpecial();
        bool showSell = CanSellTower();

        ShowUpgradeButtons(showSpeed, showRange, showDamage, showSpecial, showSell);          
    }

    private void OpenAreaTouched(OpenAreaBehavior area)
    {
        //Debug.Log("open area touched");

        _touchedArea = area;

        //Move to tower that was touched
        transform.position = _touchedArea.transform.position;        

        bool showMelee = CanBuildMelee();
        bool showRanged = CanBuildRanged();
        bool showSlow = CanBuildSlow();
        ShowBuildButtons(showMelee, showRanged, showSlow);
    }

    private  bool CanBuildMelee()
    {
        return true;
    }

    private bool CanBuildRanged()
    {
        return true;
    }

    private bool CanBuildSlow()
    {
        return true;
    }

    /// <summary>
    /// Determines if _touched tower's speed attribute can be upgraded further
    /// </summary>
    /// <returns></returns>
    private bool CanUpgradeSpeed()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);

        
        max = _maxUpgrades[index, SPEEDINDEX];

        return _touchedTower.SpeedUpgrades < max;
        
    }

    private bool CanUpgradeRange()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);

      
        max = _maxUpgrades[index, RANGEINDEX];
        return _touchedTower.RangeUpgrades < max;
    }

    private bool CanUpgradeDamage()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);

        
        max = _maxUpgrades[index, DAMAGEINDEX];

        return _touchedTower.DamageUpgrades < max;        
    }

    private bool CanUpgradeSpecial()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);
        
        max = _maxUpgrades[index, SPECIALINDEX];
        return _touchedTower.SpecialUpgrades < max;
    }

    private bool CanSellTower()
    {
        return true;
    }

    /// <summary>
    /// Displays the appropriate UI buttons to the user.
    /// </summary>
    private void ShowBuildButtons(bool melee = false, bool ranged = false, bool slow = false)
    {
        //Debug.Log("show buttons");

        _buttons[0].gameObject.SetActive(melee);
        _buttons[1].gameObject.SetActive(ranged);
        _buttons[2].gameObject.SetActive(slow);

        CheckMenuActive();
    }

    private void ShowUpgradeButtons(bool speed = false, bool range = false, bool damage = false, bool special = false, bool sell = false)
    {
        _buttons[3].gameObject.SetActive(speed);
        _buttons[4].gameObject.SetActive(range);
        _buttons[5].gameObject.SetActive(damage);
        _buttons[6].gameObject.SetActive(special);
        _buttons[7].gameObject.SetActive(sell);

        CheckMenuActive();
    }

    private void CheckMenuActive()
    {
        foreach (Button b in _buttons)
        {
            if (b.gameObject.activeSelf)
            {
                MenuActive = true;
                return;
            }
        }

        MenuActive = false;
    }
    
	
}
