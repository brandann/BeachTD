using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ManagerBase {

	protected Global _global;
	protected List<GameObject> _managerObjects;
	protected Vector3 _startingPosition = Vector3.zero;
	public Vector3 StartingPosition{ get {return _startingPosition;} }
	
	public ManagerBase()
	{
		_managerObjects = new List<GameObject>();
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
			_managerObjects.Add(spawned);
			spawned.transform.position = position;
			return go;
		}
		return null;
	}
	
	public virtual void Remove(GameObject go = null)
	{
		if(go == null)
		{
			if(_managerObjects.Count > 0)
			{
				GameObject foo = _managerObjects[0];
				_managerObjects.RemoveAt(0);
				GameObject.Destroy(foo);
			}
		}
		else if(_managerObjects.Contains(go))
		{
			_managerObjects.Remove(go);
			GameObject.Destroy(go.gameObject);
		}
		
		_global.WinLoseCond();
	}
	
	public void SetStartingPosition(Vector3 position)
	{
		_startingPosition = position;
	}
	
	public virtual int GetActiveCount()
	{
		return _managerObjects.Count;
	}
}
