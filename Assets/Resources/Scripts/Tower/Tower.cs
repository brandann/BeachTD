/* Base class for all Towers 
 * 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Place holder for Enemy base class
public class Enemy{}


public abstract class Tower : MonoBehaviour {

    public enum TowerState { Active, InActive };

    //Cost of building 
    public int Cost { public get; private set; }

    //Time between firing
    public float CoolDownTime { public get; private set; }
    
    //Current state of the tower
    public TowerState State { public get; private set;}

    //Store possible targets
    private List<Enemy> Targets;

    //Decide which target to attack
    private abstract void PrioritizeTargets();

    //Take relevant action (attack, slow etc, deploy troops etc)
    private abstract void Act(); 
    
}
