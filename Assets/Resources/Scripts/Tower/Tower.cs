/* Base class for all Towers 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{    


    #region Public Region

    public delegate void TowerEventHandler(Tower t);

    public static event TowerEventHandler onTowerTouched;

    /// <summary>
    /// Used to modify Towers real numbers are precentages but no limits are appled. Integers are "levels" defined by derived clasess
    /// </summary>
    public struct Upgrade
    {
        public float Range;
        public float Speed;
        public float Damage;
        public int Special;

        public Upgrade(float range = 0, float speed = 0, float damage = 0, int special = 0)
        {
            Range = range;
            Speed = speed;
            Damage = damage;
            Special = special;
        }
    }
    
    public float Range { get { return _collider.radius; } }

    //Damage done per hit
    public float Damage;

    //Time between firing in seconds, higher number == slower firing
    public float CoolDownTime;

    public enum TowerState { Idle, Acting, Disabled };
    
    //Current state of the tower
    public TowerState CurrentState {
        get
        {
            return _currentState;
        }
        protected set
        {
            //Debug.Log("transition from: " + _currentState + " to: " + value);
            _previousState = _currentState;            
            _currentState = value;
        }
    }    

    /// <summary>
    /// Change State to Disabled
    /// </summary>
    public virtual void DisableTower()
    {
        TransitionToState(TowerState.Disabled);
    }

    /// <summary>
    /// Changes state from Disabled to either Idle or Acting
    /// </summary>
    public virtual void EnableTower()
    {
        TransitionToState(_previousState);
    }

    /// <summary>
    /// UpgradeTower some characteristics of a tower. No upgrade limits exits on tower those are made by caller.
    /// </summary>
    /// <param name="upgrade">Attributes of upgrade == 0 will not be applied</param>
    public virtual void UpgradeTower(Upgrade upgrade)
    {
        bool upgradeapplied = false;
        if (upgrade.Range != 0)
        {
            _collider.radius *= (1 + upgrade.Range);
            upgradeapplied = true;
        }

        if (upgrade.Speed != 0)
        {
            CoolDownTime -= (CoolDownTime * upgrade.Speed);
            upgradeapplied = true;
        }

        if (upgrade.Damage != 0)
        {
            Damage *= (1 + upgrade.Damage);
            upgradeapplied = true;
        }

        if(upgrade.Special != 0){
            UpgradeSpecial(upgrade.Special);
            upgradeapplied = true;
        }
        if(upgradeapplied)
        {
            NumUpgradesApplied++;
            incrementUpgradeUI();
        }
    }

    public int NumUpgradesApplied { get; protected set; }

    #endregion

    //Animator Triggers
    protected readonly int _flashHash = Animator.StringToHash("Flash");
    protected readonly int _stopHash = Animator.StringToHash("Stop");
    protected CircleCollider2D _collider;
    protected int _speedUpgrades;
    protected int _rangeUpgrades;
    protected int _damageUpgrades;
    protected int _specialUpgrades;

    #region MonoBehaviour Region
    void Update()
    {
        if (CurrentState != TowerState.Acting)
            return;

        if (Time.time >= _nextActionTime)
            Act();
        
    }

    void Awake()
    {
        Initialize();
    }

    #endregion

    //Used to upgrade special abilities in derived classes
    protected abstract void UpgradeSpecial(int level);


    protected virtual void TransitionToState(Tower.TowerState toState)
    {
        //First transition into acting state triggers immediate action
        if (toState == TowerState.Acting)
            _nextActionTime = Time.time;

        CurrentState = toState;        

        UpdateAnimator();
    }

    protected virtual void UpdateAnimator()
    {
        switch (CurrentState)
        {
            case TowerState.Disabled:
            case TowerState.Idle:
                _anim.SetTrigger(_stopHash);
                break;
        }
    }

    public virtual void OnTouchDown()
    {
        //Debug.Log("OnTouchDown from tower");

        if (onTowerTouched != null)
            onTowerTouched(this);
        else
            Debug.Log("No subscribers");
    }

    virtual public void Initialize()
    {
        _anim = gameObject.GetComponent<Animator>();

        _collider = (CircleCollider2D)gameObject.GetComponent<Collider2D>();

        _previousState = TowerState.Idle;

        _currentState = TowerState.Idle;

        if (_anim == null)
            Debug.LogError("Missing animator");

        _targets = new List<Enemy>();

        //Reset upgrade counters
        NumUpgradesApplied = 0;    
    }
       
    //Timestamp of last action
    protected float _lastActionTime;

    //Time available to act again
    protected float _nextActionTime;

    //Animator Controller
    protected Animator _anim;

    //Store possible targets  
    protected List<Enemy> _targets;

    public List<Enemy> Targets
    {
        get { return _targets; }
    }
    
    /// <summary>
    /// CurrentState of tower before last state transition
    /// </summary>
    protected TowerState _previousState;

    protected TowerState _currentState;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();

        //Ignore collisions with non-enemies
        if (eb == null)
            return;
        

        //If we are not already targeting enemy
        if (_targets.Contains(eb) == false)
        {
            _targets.Add(eb);
            eb.ThisEnemyDied += HandleEnemyDeath;
        }
        else
        {
            //Enemy is already being targeted;
            return;
        }

        PrioritizeTargets();

        if (CurrentState == TowerState.Idle)
            TransitionToState(TowerState.Acting);
    }

    /// <summary>
    /// Handles EnemyDied events by removing them from target list
    /// </summary>
    /// <param name="enemy">enemy that just died</param>
    protected virtual void HandleEnemyDeath(Enemy enemy)
    {
        _targets.Remove(enemy);
        enemy.ThisEnemyDied -= HandleEnemyDeath;
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Enemy eb = other.gameObject.GetComponent<Enemy>();
        if (eb == null)
            return;

        _targets.Remove(eb);
        //eb.deltaTargetedCount(-1);
        if (eb == mCurrentTargetedEnemy)
        {
            mCurrentTargetedEnemy.deltaTargetedCount(-1);
            mCurrentTargetedEnemy = null;
        }
        //Debug.Log("Removed Enemy from targets");
    }

    /// <summary>
    /// Decide which target to attack; Default behavior is to target enemy that has traveled the furthest
    /// </summary>
    protected virtual void PrioritizeTargets()
    { 

        _targets.Sort(
            delegate(Enemy e1, Enemy e2)
            {
                //Null check enemies in case they have been killed but not yet removed
                float e1Dist = (e1 != null) ? e1.gameObject.GetComponent<EnemyMovement>().DistanceTraveled : 0;
                float e2Dist = (e2 != null) ? e2.gameObject.GetComponent<EnemyMovement>().DistanceTraveled : 0;
                return e2Dist.CompareTo(e1Dist);
            }
        );
    }


    /// <summary>
    /// Take relevant action (attack, slow etc, deploy troops etc)
    /// </summary>
    protected virtual void Act()
    {
        if (_targets.Count == 0){
            TransitionToState(TowerState.Idle);
            return;
        }
 
        _lastActionTime = Time.time;
        _nextActionTime = _lastActionTime + CoolDownTime;

        _anim.SetTrigger(_flashHash);
    }

    protected bool AreTargetsAvailable()
    {
        return (_targets.Count > 0);
    }

    public Enemy getTarget()
    {
        if(_targets == null)
        {
            return null;
        }

        Enemy temp = null;

        //--------------------------------------------------------------
        // check for enemies with eggs
        // look for the front most enemy first and work backwards
        if (null == temp)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (_targets[i].HasEgg)
                {
                    temp = _targets[i];
                    break;
                }
            }
        }

        //--------------------------------------------------------------
        // if temp is null then no enemies are with eggs
        // stick with the current enemy
        if (null == temp)
        {
            temp = mCurrentTargetedEnemy;
        }

        //--------------------------------------------------------------
        // check for enemies without targets
        // look for the front most enemy first and work backwards
        if (null == temp)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (_targets[i].getTargetedCount() == 0)
                {
                    temp = _targets[i];
                    break;
                }
            }
        }
        
        //--------------------------------------------------------------
        // if temp is null then no enemies are un targeted
        // get the front most enemy at this point
        if(null == temp)
        {
            temp = _targets[0];
        }

        //--------------------------------------------------------------
        // if temp and current are not the same enemy then:
        //      - deselect current
        //      - select temp
        //      - set current as temp
        if(temp != mCurrentTargetedEnemy)
        {
            if(null != mCurrentTargetedEnemy)
            {
                mCurrentTargetedEnemy.deltaTargetedCount(-1);
            }
            mCurrentTargetedEnemy = temp;
            mCurrentTargetedEnemy.deltaTargetedCount(1);
        }

        //--------------------------------------------------------------
        // return the current enemy
        return mCurrentTargetedEnemy;
    }

    private Enemy mCurrentTargetedEnemy;

    public Sprite ToggleOnSprite;
    public GameObject Toggle1;
    public GameObject Toggle2;

    private void incrementUpgradeUI()
    {
        if(NumUpgradesApplied == 1)
        {
            // first upgrade
            Toggle1.SetActive(true);
            Toggle2.SetActive(true);
        }
        else if(NumUpgradesApplied == 2)
        {
            //second upgrade
            Toggle2.GetComponent<SpriteRenderer>().sprite = ToggleOnSprite;
        }

        //different way to do it
        /*
        if (NumUpgradesApplied == 1)
        {
            // first upgrade
            Toggle1.SetActive(true);
        }
        else if (NumUpgradesApplied == 2)
        {
            //second upgrade
            Toggle2.SetActive(true);
        }
        */
    }

    public void SetTowerInactive()
    {
        Toggle1.SetActive(false);
        Toggle2.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
