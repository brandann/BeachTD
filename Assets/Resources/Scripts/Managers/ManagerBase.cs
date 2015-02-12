using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ManagerBase {

	protected Global _global;
	protected Dictionary<int, GameObject> _managerObjects;
	protected Vector3 _startingPosition = Vector3.zero;
	
	public ManagerBase()
	{
		_managerObjects = new Dictionary<int, GameObject>();
		_global = GameObject.Find("Global").GetComponent<Global>();
		//_startingPosition = _global.CurrentMap.Waypoints[0];
	}
	
	public void Reset()
	{
		if (_managerObjects != null)
		{
			for (int i = 0; i < _managerObjects.Count; i++)
			{
				GameObject.Destroy(_managerObjects[i]);
			}
		}
	}
	
	public virtual GameObject Create(GameObject go, Vector3 position)
	{
		if(go != null)
		{
			GameObject spawned = GameObject.Instantiate(go) as GameObject;
			_managerObjects.Add(spawned.GetInstanceID(), spawned);
			spawned.transform.position = position;
			return go;
		}
		return null;
	}
	
	public virtual void Remove(GameObject go)
	{
		if(_managerObjects.ContainsKey(go.GetInstanceID()))
		{
			_managerObjects.Remove(go.GetInstanceID());
		}
	}
	
	public void SetStartingPosition(Vector3 position)
	{
		_startingPosition = position;
	}
	
	public int GetActiveCount()
	{
		return _managerObjects.Count;
	}
}
