using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

 	// hold the instance of the "touched" tower
	TowerBehavior orgTower;
  
  	// hold the instance of the new tower
	TowerBehavior upgrade;
  
  // instances of TowerIcons
  // used for Destroy at end of Manager
	GameObject btn1;
	GameObject btn2;
	GameObject btn3;
	GameObject btn4;
	
	float btnspace = .5f;
	Vector3 btn1pos;
	Vector3 btn2pos;
	Vector3 btn3pos;
	Vector3 btn4pos;

	// Use this for initialization
	void Start () {
		btn1pos = new Vector3( -btnspace,  btnspace);
		btn2pos = new Vector3(  btnspace,  btnspace);
		btn3pos = new Vector3( -btnspace, -btnspace);
		btn4pos = new Vector3(  btnspace, -btnspace);
		
		Debug.Log("TowerManager Created");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// set orgTower to the original tower that was touched
  // this is the tower to be replaced at end of manager cycle
  // can be Tower or OpenSpace
	public void setOrgTower(GameObject go)
	{
		// Towerobject from GameObject
		orgTower = go.GetComponent<TowerBehavior>();
		
		// get all the available upgrades from the original tower
		GameObject[] upgrades = orgTower.getUpgrades();
		
		// make icon buttons
		btn1 = setButton(upgrades[0], btn1pos);
		btn2 = setButton(upgrades[1], btn2pos);
		btn3 = setButton(upgrades[2], btn3pos);
		btn4 = setButton(upgrades[3], btn4pos);
	}
	
  // setUpgradeTower to the tower to be built
  // go is passed in with then TowerIcon when touched
  // can be Tower or OpenSpace
	public void setUpgradeTower(GameObject go)
	{
	
	}
	
  // generic method to set TowerIcons around the selected tower
	private GameObject setButton(GameObject go, Vector3 pos)
	{
		// if go is null, then no icon exists at this location
		// return null to keep nothing in this position
		if(go == null)
		{
			return null;
		}
		
		// get icon from gameobject
		GameObject i = go.GetComponent<TowerBehavior>().getTowerIcon();
		
		// instantiate icon for menu
		GameObject icon = Instantiate(i) as GameObject;
		
		// set the icons position relative to the orginal towers position
		icon.transform.position += pos;
		
		// pass the Towericon information to know status of menu
		TowerIcon iconTower = icon.GetComponent<TowerIcon>();
		iconTower.setUpgrade(go);
		iconTower.setManager(this.gameObject);
		
		// return the towericon
		return icon;
	}
	
  // Confirm commits the change from orgTower to UpgradeTower
  // can be Tower or OpenSpace
  // double check notes for use of 'go'
	public void Confirm(GameObject go)
	{
		// destroy all icons
		Destroy(btn1.gameObject);
		Destroy(btn2.gameObject);
		Destroy(btn3.gameObject);
		Destroy(btn4.gameObject);
		
		//instantiate Confirm
	}
	
	
}
