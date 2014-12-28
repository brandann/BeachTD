using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

	public GameObject[] TowerUpgrades;
	public GameObject TowerIcon;
	public int Cost;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Destroy myself
	// a simple destroy method that finishes any tasks that
	// need to be done before the tower is completly gone.
	public void destroy()
	{
		Destroy(this.gameObject);
	}
	
	void OnTouchDown()
	{
		// will instantiate a new tower manager that will create a
		// menu system to: upgrade, sell, or build towers.
		// pass this GameObject to manager to provide manager with a refrence to
		// this for Destroying and replacing
		//GameObject towerPrefab = Resources.Load("Prefabs/TowerManagerPrefab") as GameObject;
		//GameObject e = Instantiate(towerPrefab) as GameObject;
		//e.transform.position = this.transform.position;
		
		GameObject tower = Resources.Load("Prefabs/TowerA") as GameObject;
		GameObject e = Instantiate(tower) as GameObject;
		e.transform.position = this.transform.position;
		
		destroy();
	}
	
	// returns an array of available upgrades for this tower
	// used to display to the manager what upgrades are available
	// for a tower
	public GameObject[] getUpgrades()
	{
		return TowerUpgrades;
	}
	
	// returns an icon for this tower
	// icon knows this tower and can hold the GameObject information
	// for the manager
	public GameObject getTowerIcon()
	{
		return TowerIcon;
	}
	
	// returns the cost of the tower
	// this is the full cost that the tower costs,
	// can be negative if the player gets a return, or positive
	// if the player needs to pay, or zero if the tower has no impact on the
	// players "wallet"
	public int getCost()
	{
		return Cost;
	}
}
