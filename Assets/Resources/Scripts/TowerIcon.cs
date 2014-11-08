using UnityEngine;
using System.Collections;

public class TowerIcon : MonoBehaviour {

	GameObject Manager;
	GameObject Upgrade;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setUpgrade(GameObject go)
	{
		Upgrade = go;
	}
	
	public void setManager(GameObject go)
	{
		Manager = go;
	}
	
	public void OnTouch()
	{
		Manager.GetComponent<TowerManager>().Confirm(Upgrade);
	}
}
