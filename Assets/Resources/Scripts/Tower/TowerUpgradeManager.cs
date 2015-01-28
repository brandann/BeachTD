using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Determines the availabilty of upgrades based on tower selection and funds available etc.
/// Controls the presentation of the upgrade UI elements and handles the user selection thereof.
/// </summary>
public class TowerUpgradeManager : MonoBehaviour {

    public GameObject MeleePrefab;
    public GameObject RangedPrefab;
    public GameObject SlowPrefab;

    private Tower _touchedTower;
    private Button _meleeUpButton;
    private Button _rangedUpButton;
    private Button _slowUpButton;

    private Button[] _buttons;

    public void MeleeUpgrade()
    {
       
    }

    void Awake()
    {
        //Assign buttons
        _buttons = new Button[3];
        _buttons[0] = _meleeUpButton;
        _buttons[1] = _rangedUpButton;
        _buttons[2] = _slowUpButton;
    }

    void OnEnable()
    {
        Tower.onTowerTouched += TowerTouched;
    }

    void OnDisable()
    {
        Tower.onTowerTouched -= TowerTouched;
    }

    private void TowerTouched(Tower tower)
    {
        //Ignore disabled towers
        if (tower.CurrentState == Tower.TowerState.Disabled)
            return;

        //Cache reference to tower 
        _touchedTower = tower;

        transform.position = tower.transform.position;

        bool showMelee = IsMeleeUpgradeable(tower);
        bool showRanged = IsRangedUpgradeable(tower);
        bool showSlow = IsSlowUpgradeable(tower);
        ShowButtons(showMelee, showRanged, showSlow);
        
    }

    private  bool IsMeleeUpgradeable(Tower tower)
    {
        return (tower is MeleeTower);
    }

    private bool IsRangedUpgradeable(Tower tower)
    {
        return (tower.gameObject.name.Contains("Ranged"));
    }

    private bool IsSlowUpgradeable(Tower tower)
    {
        return (tower.gameObject.name.Contains("Slow"));
    }


    /// <summary>
    /// Displays the appropriate UI buttons to the user.
    /// </summary>
    private void ShowButtons(bool melee, bool ranged, bool slow)
    {
        _buttons[0].gameObject.SetActive(melee);
        _buttons[1].gameObject.SetActive(ranged);
        _buttons[2].gameObject.SetActive(slow);
    }




}
