using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerCostUI : MonoBehaviour {

	public enum TowerUI {Melee, Range, Slow}
	public TowerUI towerUI;
	public GameObject towers;
	
	// Use this for initialization
	void Start () {
		var g = towers.GetComponent<TowerUpgradeManager>();
		int cost = 0;
		if(towerUI == TowerUI.Melee)
		{
			cost = g.MeleeTowerCost;
		}
		else if (towerUI == TowerUI.Range)
		{
			cost = g.RangedTowerCost;
		}
		else if(towerUI == TowerUI.Slow)
		{
			cost = g.SlowTowerCost;
		}
		this.GetComponent<Text>().text = "$" + cost;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
