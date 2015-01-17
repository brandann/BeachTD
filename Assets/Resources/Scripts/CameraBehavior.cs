using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	private bool zoom = false;
	Global global;

	// Use this for initialization
	void Start () 
	{
		global = GameObject.Find("Global").GetComponent<Global>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.E))
		{
			zoom = true;
		}	
		
		if(zoom)
		{
			float m_height = global.CurrentMap.Height + 1;
			float m_width = global.CurrentMap.Width + 1;
			float c_height = camera.orthographicSize;
			float c_width = c_height * camera.aspect;
			float p_height = m_height;
			float p_width = p_height * camera.aspect;
			
			if( p_width < m_width)
			{
				// the lap is to long for the camera
				this.camera.orthographicSize = (m_width / camera.aspect) / 2f;
				
			}
			else
			{
				// the map fits into the camera
				this.camera.orthographicSize = m_height / 2f;
			}
			
			//this.camera.orthographicSize += 1;
			this.transform.position = new Vector3(((camera.orthographicSize * 2) * camera.aspect) / 2f, camera.orthographicSize, -10f);
			
			
			
			//this.transform.position = new Vector3(transform.position.x, this.camera.orthographicSize, -10);
			//Debug.Log(camera.aspect);
			/*this.transform.position = new Vector3(
				(this.camera.orthographic * 2 * this.camera.aspect) / 2,
				this.camera.orthographicSize,
				-10f);*/
			zoom = !zoom;
		}
		
	}
}
