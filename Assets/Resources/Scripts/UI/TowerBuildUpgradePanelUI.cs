using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBuildUpgradePanelUI : MonoBehaviour
{

    #region InspectorAssigned

    public Button BuildMeleeButton;
    public Button BuildRangedButton;
    public Button BuildSlowButton;
    public Button SpeedUpButton;
    private Button RangeUpButton;
    private Button DamageUpButton;
    private Button SpecialUpButton;
    public Button SellButton;
    public float FADETIME;

    #endregion
    
    public bool MenuActive { get; private set; }

    #region ButtonOnClicks
    public void SellTower()
    {
        if (_touchedTower == null)
        {
            Debug.LogWarning("Missing tower");
            return;
        }
        
        _towerManager.SellTower(_touchedTower);
        ShowUpgradeButtons();
    }

    public void Upgrade()
    {
        //Clear UI 
        ShowUpgradeButtons();
        _towerManager.UpgradeTower(_touchedTower);
    }

    public void BuildMeleeClicked(){
        //Clear UI
        ShowBuildButtons();

        _towerManager.BuildMeleeFromArea(_touchedArea);
    }

    public void BuildRangedClicked()
    {
        //Clear UI
        ShowBuildButtons();
        _towerManager.BuildRangedFromArea(_touchedArea);

    }

    public void BuildSlowClicked()
    {
        //Clear UI
        ShowBuildButtons();
        _towerManager.BuildSlowFromArea(_touchedArea);
    }

    #endregion

    #region UnityCallbacks

    void Awake()
    {
        _towerManager = gameObject.GetComponent<TowerUpgradeManager>();
        if (_towerManager == null)
            Debug.LogWarning("Missing towermanager");

        CacheButtons();

        //with no params actually hides all buttons
        ShowBuildButtons();
        ShowUpgradeButtons();
    }

    private void Update()
    {
        if (MenuActive && Time.time >= _lastActivation + FADETIME)
            DeselectAndHide();
    }


    void OnEnable()
    {
        Path.OnPathTouched += HandlePathTouched;
        Tower.onTowerTouched += TowerTouched;
        OpenAreaBehavior.onAreaTouched += OpenAreaTouched;
        Seagull.OnGullKilled += HandleGullHit;
    }

    void OnDestroy()
    {
        Path.OnPathTouched -= HandlePathTouched;
        Tower.onTowerTouched -= TowerTouched;
        OpenAreaBehavior.onAreaTouched -= OpenAreaTouched;
        Seagull.OnGullKilled -= HandleGullHit;
    }

    #endregion



    private Button[] _buttons;
    private Vector2[] _defaultButtonPositions;
    private Vector2 _buttonOffset;
    private float _lastActivation;
    private TowerUpgradeManager _towerManager;
    //Last tower that fired touch event
    private Tower _touchedTower;
    //Last open area that fired touch event
    private OpenAreaBehavior _touchedArea;
    private Vector2 _buttonSizeRect = new Vector2(0.5f, 0.5f);
    private Collider2D[] _hitByButtons;
    private float _lastGullHitTime;

    private void CacheButtons()
    {
        //Assign buttons
        _buttons = new Button[5];
        _defaultButtonPositions = new Vector2[5];


        _buttons[0] = BuildMeleeButton;
        _buttons[1] = BuildRangedButton;
        _buttons[2] = BuildSlowButton;
        _buttons[3] = SpeedUpButton;
        _buttons[4] = SellButton;

        for (int i = 0; i < _buttons.Length; ++i)
            _defaultButtonPositions[i] = _buttons[i].transform.position;

        Vector2 centerOfBuilder = transform.position;

        //Assume that buildmelee button starts off to the right and above
        _buttonOffset = new Vector2(1.5f, 0.9f);



        //Ensure all buttons have been added in the inspector
        foreach (Button b in _buttons)
            if (b.gameObject == null)
            {
                Debug.LogError("Missing button");
            }
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

            if (r != null)
            {
                target = r.bounds;
            }
            else
            {
                target = go.GetComponent<Collider2D>().bounds;
            }

            if (b.GetComponent<Collider2D>().bounds.Intersects(target))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
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

    private void TowerTouched(Tower tower)
    {

        //Ignore disabled towers
        if (tower.CurrentState == Tower.TowerState.Disabled)
            return;

        if (MenuActive)
        {
            //Tower was touched when user was clicking on an active button
            if (IsBelowButton(tower.gameObject))
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

    private void SelectAndShow(Tower t)
    {
        //Cache reference to tower 
        _touchedTower = t;

        //Move to tower that was touched
        //transform.position = t.transform.position;
        //ShiftButtons();

        bool showSpeed = _towerManager.CanUpgrade(t);
        
        bool showSell = _towerManager.CanSellTower(t);

        ShowUpgradeButtons(showSpeed, false, false, false, showSell);
        _lastActivation = Time.time;
    }

    private void SelectAndShow(OpenAreaBehavior a)
    {
        _touchedArea = a;

        //Move to open area that was touched
        //transform.position = _touchedArea.transform.position;
        //ShiftButtons();

        bool showMelee = _towerManager.CanBuildMelee();
        bool showRanged = _towerManager.CanBuildRanged();
        bool showSlow = _towerManager.CanBuildSlow();
        ShowBuildButtons(showMelee, showRanged, showSlow);
        _lastActivation = Time.time;
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
                //Debug.Log("Show menu: " + Time.time);
            }

        //Debug.Log("open area touched");      
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

        if (MenuActive)
            _lastActivation = Time.time;
    }

    private void ShowUpgradeButtons(bool speed = false, bool range = false, bool damage = false, bool special = false, bool sell = false)
    {

        _buttons[3].gameObject.SetActive(speed);
        _buttons[4].gameObject.SetActive(sell);

        CheckMenuActive();

        if (MenuActive)
            _lastActivation = Time.time;

    }

    private void HandleGullHit(Seagull gull)
    {
        _lastGullHitTime = Time.time;
    }

    private void DeselectAndHide()
    {
        ShowBuildButtons();
        ShowUpgradeButtons();
        _touchedTower = null;
        _touchedArea = null;
    }
    //Moves buttons away from edge of world
    private void ShiftButtons()
    {
        //Debug.Log("Menu at x: " + transform.position.x + " y: " + transform.position.y);

        if (transform.position.x > 13.5)
        {
            SlideButtonsLeft();
        }
        else
        {
            ButonsToDefaultXPos();
        }

        if (transform.position.y > 7)
        {
            SlideButtonsDown();
        }
        else
        {
            if (transform.position.y < 0.8)
                SlideButtonsUp();
            else
                ButonsToDefaultYPos();
        }

    }

    private void SlideButtonsLeft()
    {
        /*
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].transform.position = new Vector2(_defaultButtonPositions[i].x - _buttonOffset.x, _defaultButtonPositions[i].y); 
        }
         */
        transform.position = (Vector2)transform.position - new Vector2(_buttonOffset.x, 0);

        //Debug.Log("slid left");
    }

    private void SlideButtonsDown()
    {
        /*
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].transform.position = new Vector2(_defaultButtonPositions[i].x, _defaultButtonPositions[i].y - _buttonOffset.y);
        }
        
         */
        //Debug.Log("slid down");
        transform.position = (Vector2)transform.position - new Vector2(0, _buttonOffset.y);
    }

    private void SlideButtonsUp()
    {
        /*
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].transform.position = new Vector2(_defaultButtonPositions[i].x, _defaultButtonPositions[i].y + _buttonOffset.y);
        }
        
         */
        //Debug.Log("slid down");
        transform.position = (Vector2)transform.position + new Vector2(0, _buttonOffset.y);
    }



    private void ButonsToDefaultXPos()
    {
        /*
        Debug.Log("Default X Positions");
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].transform.position = new Vector2(_defaultButtonPositions[i].x, transform.position.y);
        }
         */

    }

    private void ButonsToDefaultYPos()
    {
        /*
        Debug.Log("Default Y Positions");
        for (int i = 0; i < _buttons.Length; ++i)
        {
            _buttons[i].transform.position = new Vector2(transform.position.x, _defaultButtonPositions[i].y);
        }
         */
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
