using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

  // hold the instance of the "touched" tower
	GameObject orgTower;
  
  // hold the instance of the new tower
	GameObject upgrade;
  
  // instances of TowerIcons
  // used for Destroy at end of Manager
	GameObject btn1;
	GameObject btn2;
	GameObject btn3;
	GameObject btn4;
	
	public GameObject towerAIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// set orgTower to the original tower that was touched
  // this is the tower to be replaced at end of manager cycle
  // can be Tower or OpenSpace
	public void setOrgTower(GameObject go)
	{
		orgTower = go;
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
		return null;
	}
	
  // Confirm commits the change from orgTower to UpgradeTower
  // can be Tower or OpenSpace
  // double check notes for use of 'go'
	public void Confirm(GameObject go)
	{
	
	}
	
	
}
