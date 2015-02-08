using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Determines the availabilty of upgrades based on tower selection and funds available etc.
/// Controls the presentation of the upgrade UI elements and handles the user selection thereof.
/// </summary>
public class TowerUpgradeManager : MonoBehaviour
{
    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;

    public Button BuildMeleeButton;
    public Button BuildRangedButton;
    public Button BuildSlowButton;
    public Button SpeedUpButton;
    public Button RangeUpButton;
    public Button DamageUpButton;
    public Button SpecialUpButton;

    private Tower _touchedTower;
    private OpenAreaBehavior _touchedArea;
    
    private readonly Tower.Upgrade _specialUpgrade = new Tower.Upgrade(0,0,0,1);
    private readonly Tower.Upgrade _10percentSpeed = new Tower.Upgrade(0,.1f);
    private readonly Tower.Upgrade _10percentDamage = new Tower.Upgrade(0,0,0.1f);
    private readonly Tower.Upgrade _10percentRange = new Tower.Upgrade(0.1f);


    private Button[] _buttons;

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

    void Awake()
    {
        //Assign buttons
        _buttons = new Button[7];
        _buttons[0] = BuildMeleeButton;
        _buttons[1] = BuildRangedButton;
        _buttons[2] = BuildSlowButton;
        _buttons[3] = SpeedUpButton;
        _buttons[4] = RangeUpButton;
        _buttons[5] = DamageUpButton;
        _buttons[6] = SpecialUpButton;

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
        ShowUpgradeButtons(showSpeed, showRange, showDamage, showSpecial);          
    }

    private void OpenAreaTouched(OpenAreaBehavior area)
    {
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

    private bool CanUpgradeSpeed()
    {
        return true;
    }

    private bool CanUpgradeRange()
    {
        return true;
    }

    private bool CanUpgradeDamage()
    {
        return true;
    }

    private bool CanUpgradeSpecial()
    {
        return true;
    }

    /// <summary>
    /// Displays the appropriate UI buttons to the user.
    /// </summary>
    private void ShowBuildButtons(bool melee = false, bool ranged = false, bool slow = false)
    {
        
        if (_buttons[0].gameObject != null)
        {
            _buttons[0].gameObject.SetActive(melee);
        }

        if (_buttons[1].gameObject != null)
        {
            _buttons[1].gameObject.SetActive(ranged);
        }

        if(_buttons[2].gameObject != null)
        {
            _buttons[2].gameObject.SetActive(slow);
        }

    }

    private void ShowUpgradeButtons(bool speed = false, bool range = false, bool damage = false, bool special = false)
    {
        _buttons[3].gameObject.SetActive(speed);
        _buttons[4].gameObject.SetActive(range);
        _buttons[5].gameObject.SetActive(damage);
        _buttons[6].gameObject.SetActive(special);
    }
}
