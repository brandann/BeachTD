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

    public Button MeleeUpButton;
    public Button RangedUpButton;
    public Button SlowUpButton;

    private Tower _touchedTower;
    private OpenAreaBehavior _touchedArea;
    

    private Button[] _buttons;

    public void BuildMelee()
    {       

        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(MeleePrefab.GetComponent<MeleeTower>());
        tower.transform.position= _touchedArea.transform.position;

        //Out with the old
        Destroy(_touchedArea.gameObject);
        _touchedArea = null;       
    }

    public void BuildRanged()
    {
        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(RangedPrefab.GetComponent<RangedTower>());
        tower.transform.position = _touchedArea.transform.position;

        //Out with the old
        Destroy(_touchedArea.gameObject);
        _touchedArea = null;

    }

    public void BuildSlow()
    {
        //In with the new
        Tower tower = TowerFactory.Instance.CreateTower(SlowPrefab.GetComponent<SlowTower>());
        tower.transform.position = _touchedArea.transform.position;

        //Out with the old
        Destroy(_touchedArea.gameObject);
        _touchedArea = null;
    }



    void Awake()
    {
        //Assign buttons
        _buttons = new Button[3];
        _buttons[0] = MeleeUpButton;
        _buttons[1] = RangedUpButton;
        _buttons[2] = SlowUpButton;

        //with no params actually hides all buttons
        //ShowButtons();
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
    
        
    }

    private void OpenAreaTouched(OpenAreaBehavior area)
    {
        _touchedArea = area;

        //Move to tower that was touched
        transform.position = _touchedArea.transform.position;


        bool showMelee = CanBuildMelee();
        bool showRanged = CanBuildRanged();
        bool showSlow = CanBuildSlow();
        ShowButtons(showMelee, showRanged, showSlow);    

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
    /// Displays the appropriate UI buttons to the user.
    /// </summary>
    private void ShowButtons(bool melee = false, bool ranged = false, bool slow = false)
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

    




}
