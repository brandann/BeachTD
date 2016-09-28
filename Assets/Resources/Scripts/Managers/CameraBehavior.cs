using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	private bool zoom = false;
	//Global _global;
	float speed = .0001f;
	Vector2 lasttouch;

	// Use this for initialization
	void Start () 
	{
		//_global = GameObject.Find("Global").GetComponent<Global>();
#if UNITY_IOS
        GetComponent<Camera>().orthographicSize = 5.3f;
        //GetComponent<Camera>().aspect = 4f / 3f;
#else
        GetComponent<Camera>().orthographicSize = 4;
        //GetComponent<Camera>().aspect = 16f / 9f;
#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.E))
		{
			//zoom = true;
		}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 trans = Input.GetTouch(0).deltaPosition - lasttouch;
            trans = trans * speed * Time.deltaTime;
            lasttouch = Input.GetTouch(0).deltaPosition;
            transform.Translate(trans.x, trans.y * speed, 0);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            Vector2 trans = pos - lasttouch;
            trans = trans * speed * Time.deltaTime;
            lasttouch = Input.mousePosition;
            transform.Translate(-trans.x, -trans.y * speed, 0);
            //Vector2 touchDeltaPosition = Input.mousePosition;
            //transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }

        if (zoom)
		{
			float m_height = 9;
			float m_width = 9 * 1.77f;
			//float c_height = camera.orthographicSize;
			//float c_width = c_height * camera.aspect;
			float p_height = m_height;
			float p_width = p_height * GetComponent<Camera>().aspect;
			
			if( p_width < m_width)
			{
				// the lap is to long for the camera
				this.GetComponent<Camera>().orthographicSize = (m_width / GetComponent<Camera>().aspect) / 2f;
				
			}
			else
			{
				// the map fits into the camera
				this.GetComponent<Camera>().orthographicSize = m_height / 2f;
			}
			
			//this.camera.orthographicSize += 1;
			this.transform.position = new Vector3(((GetComponent<Camera>().orthographicSize * 2) * GetComponent<Camera>().aspect) / 2f, GetComponent<Camera>().orthographicSize, -10f);
			
			zoom = !zoom;

		}
		
	}
}
