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

    public Button BuildMeleeButton;
    public Button BuildRangedButton;
    public Button BuildSlowButton;
    public Button SpeedUpButton;
    private Button RangeUpButton;
    private Button DamageUpButton;
    private Button SpecialUpButton;
    public Button SellButton;

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

    public bool MenuActive { get; private set; }

    #region MonoBehaviour
    void Awake()
    {
        _specialUpgrade = new Tower.Upgrade(0, 0, 0, 1);
        _10percentSpeed = new Tower.Upgrade(0, SpeedUpgradePercentage);
        _10percentDamage = new Tower.Upgrade(0, 0, DamageUpgradePercentage);
        _10percentRange = new Tower.Upgrade(0.3f);

        AssignButtons();
        FillLookups();       

        if (MeleeTowerCost < 0 || RangedTowerCost < 0 || SlowTowerCost < 0)
            Debug.LogWarning("Forgot to set build costs in upgrade manager?");

        //if (RangeUpgradeCost < 0 || UpgradeCost < 0 || DamageUpgradeCost < 0 || SpecialUpgradeCost < 0)
        //    Debug.LogWarning("Forgot to set upgrade costs in upgrade manager?");

        if (UpgradeCost < 0)
            Debug.LogWarning("Forgot to set upgrade costs in upgrade manager?");

        //with no params actually hides all buttons
        ShowBuildButtons();
        ShowUpgradeButtons();
    }

    void Start()
    {
        _bank = GameObject.Find("Global").GetComponent<SandDollarBank>();
        if (_bank == null)
            Debug.LogError("Can't find bank");
    }

    void OnEnable()
    {
        Tower.onTowerTouched += TowerTouched;
        OpenAreaBehavior.onAreaTouched += OpenAreaTouched;
        Path.OnPathTouched += HandlePathTouched;
        Seagull.OnGullKilled += HandleGullHit;
        
    }

    void OnDestroy()
    {
        Tower.onTowerTouched -= TowerTouched;
        OpenAreaBehavior.onAreaTouched -= OpenAreaTouched;
        Path.OnPathTouched -= HandlePathTouched;
        Seagull.OnGullKilled -= HandleGullHit;
    }

    #endregion

    #region Buttons

    public void BuildMelee()
    {
        BuildTower(MeleePrefab.GetComponent<MeleeTower>());
        _bank.SubtractDollars(MeleeTowerCost);
    }

    public void BuildRanged()
    {
        BuildTower(RangedPrefab.GetComponent<RangedTower>());
        _bank.SubtractDollars(RangedTowerCost);
    }

    public void BuildSlow()
    {
        BuildTower(SlowPrefab.GetComponent<SlowTower>());
        _bank.SubtractDollars(SlowTowerCost);
    }

    public void UpgradeSpeed()
    {
        UpgradeTower(_10percentSpeed);
        UpgradeTower(_10percentRange);
        _bank.SubtractDollars(UpgradeCost);
    }

    public void UpgradeRange()
    {
        //UpgradeTower(_10percentRange);
        //_bank.SubtractDollars(RangeUpgradeCost);
    }

    public void UpgradeDamage()
    {
        //UpgradeTower(_10percentDamage);
        //_bank.SubtractDollars(DamageUpgradeCost);
    }

    public void UpgradeSpecial()
    {
        //UpgradeTower(_specialUpgrade);
        //_bank.SubtractDollars(SpecialUpgradeCost);
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

        //TODO  give player money for selling tower
    }

    #endregion

    //Last tower that fired touch event
    private Tower _touchedTower;
    
    //Last open area that fired touch event
    private OpenAreaBehavior _touchedArea;

    private Tower.Upgrade _specialUpgrade;
    private Tower.Upgrade _10percentSpeed;
    private Tower.Upgrade _10percentDamage;
    private Tower.Upgrade _10percentRange;

    private Dictionary<Tower, int> _towerLookup;

    private int[,] _maxUpgrades;
    private readonly float FADETIME = 5f;
    private readonly int RANGEINDEX = 0;
    private readonly int SPEEDINDEX = 1;
    private readonly int DAMAGEINDEX = 2;
    private readonly int SPECIALINDEX = 3;
    private Button[] _buttons;
    private Vector2 _buttonSizeRect = new Vector2(0.5f, 0.5f);
    private Collider2D[] _hitByButtons;
    private SandDollarBank _bank;
    private float _lastActivation;
    private float _lastGullHitTime;

    public void Update()
    {
        if (MenuActive && Time.time >= _lastActivation + FADETIME)
            DeselectAndHide();        
    }


    private void AssignButtons()
    {
        //Assign buttons
        _buttons = new Button[5];
        _buttons[0] = BuildMeleeButton;
        _buttons[1] = BuildRangedButton;
        _buttons[2] = BuildSlowButton;
        _buttons[3] = SpeedUpButton;
        //_buttons[4] = null; //_buttons[4] = RangeUpButton;
        //_buttons[5] = null; //_buttons[5] = DamageUpButton;
        //_buttons[6] = null; //_buttons[6] = SpecialUpButton;
        _buttons[4] = SellButton;

        //Ensure all buttons have been added in the inspector
        foreach (Button b in _buttons)
            if (b.gameObject == null)
            {
                Debug.LogError("Missing button");
            }
    }

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
        
        // call enemy manager and tell her a tower has been built
        Global go = GameObject.Find("Global").GetComponent<Global>();
        go.enemyManager.NotifyTowerBuilt();
        GameObject.Find("Seagull").GetComponent<Seagull>().StartPlane();
    }

    private void DeselectAndHide()
    {
        ShowBuildButtons();
        ShowUpgradeButtons();
        _touchedTower = null;
        _touchedArea = null;
    }

    private void SelectAndShow(Tower t)
    {
        //Cache reference to tower 
        _touchedTower = t;

        //Move to tower that was touched
        transform.position = t.transform.position;

        bool showSpeed = CanUpgradeSpeed();
        bool showRange = CanUpgradeRange();
        bool showDamage = CanUpgradeDamage();
        bool showSpecial = CanUpgradeSpecial();
        bool showSell = CanSellTower();

        ShowUpgradeButtons(showSpeed, showRange, showDamage, showSpecial, showSell);
        _lastActivation = Time.time;
    }

    private void SelectAndShow(OpenAreaBehavior a)
    {
        _touchedArea = a;

        //Move to open area that was touched
        transform.position = _touchedArea.transform.position;

        bool showMelee = CanBuildMelee();
        bool showRanged = CanBuildRanged();
        bool showSlow = CanBuildSlow();
        ShowBuildButtons(showMelee, showRanged, showSlow);
        _lastActivation = Time.time;
    }

    private void HandlePathTouched(Path path)
    {
        if (MenuActive)
        {
            //Ignore hitting path when trying to select button
            if (IsBelowButton(path.gameObject))
                return;
            else
                DeselectAndHide();
        }           
       
    }

    private void HandleGullHit(Seagull gull)
    {
        _lastGullHitTime = Time.time;
        Debug.Log("logged gull hit: " + _lastGullHitTime);
    }


    private void TowerTouched(Tower tower)
    {

        //Ignore disabled towers
        if (tower.CurrentState == Tower.TowerState.Disabled)
            return;        

        if(MenuActive)
        {           
            //Tower was touched when user was clicking on an active button
            if ( IsBelowButton(tower.gameObject) )
                return;
            else                         //Menu up but user chose something else
            {
                DeselectAndHide();               
            }

            return;
        }
        else
            if (_lastGullHitTime != Time.time)
            {
                SelectAndShow(tower);
                Debug.Log("Show menu: " + Time.time);
            }
                  
    }

    private void OpenAreaTouched(OpenAreaBehavior area)
    {
        if (MenuActive)
        {
            if (IsBelowButton(area.gameObject))
                return;
            else
                DeselectAndHide();
        }
        else
            if (_lastGullHitTime != Time.time)
            {
                SelectAndShow(area);
                Debug.Log("Show menu: " + Time.time);
            }
        
        //Debug.Log("open area touched");      
    }

    /// <summary>
    /// Checks worldspace below each active button for passed in gameobject
    /// </summary>
    /// <param name="go"></param>
    /// <returns>true if object is below any active button false otherwise</returns>
    private bool IsBelowButton(GameObject go)
    {
        foreach (Button b in _buttons)
        {
            //Ignore buttons that aren't active
            if (!b.gameObject.activeSelf)
                continue;

            Renderer r = go.GetComponent<Renderer>();
            Bounds target;

            if(r != null)
            {
                target = r.bounds;
            }
            else
            {
                target =  go.GetComponent<Collider2D>().bounds;
            }           
                                    
            if (b.GetComponent<Collider2D>().bounds.Intersects(target) )            
                return true;           
        }

        return false;
    }

    private  bool CanBuildMelee()
    {
        return _bank.SandDollars >= MeleeTowerCost;
    }

    private bool CanBuildRanged()
    {
        return _bank.SandDollars >= RangedTowerCost;
    }

    private bool CanBuildSlow()
    {
        return _bank.SandDollars >= SlowTowerCost;
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


        return (_touchedTower.SpeedUpgrades < max) && (UpgradeCost <= _bank.SandDollars);        

    }

    private bool CanUpgradeRange()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);

      
        max = _maxUpgrades[index, RANGEINDEX];

        return false; // return (_touchedTower.RangeUpgrades < max) && (RangeUpgradeCost <= _bank.SandDollars);
    }

    private bool CanUpgradeDamage()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);

        
        max = _maxUpgrades[index, DAMAGEINDEX];

        return false; // return (_touchedTower.DamageUpgrades < max) && (DamageUpgradeCost <= _bank.SandDollars);    
    }

    private bool CanUpgradeSpecial()
    {
        int index = -1, max = -1;
        _towerLookup.TryGetValue(_touchedTower, out index);
        
        max = _maxUpgrades[index, SPECIALINDEX];

        return false; // return (_touchedTower.SpecialUpgrades < max) && (UpgradeCost <= _bank.SandDollars);
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
        //_buttons[4].gameObject.SetActive(range);
        //_buttons[5].gameObject.SetActive(damage);
        //_buttons[6].gameObject.SetActive(special);
        _buttons[4].gameObject.SetActive(sell);

        CheckMenuActive();

        if (MenuActive)
            _lastActivation = Time.time;

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
