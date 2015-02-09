using UnityEngine;
using System.Collections;

public class CharAnimation : MonoBehaviour {

	float scale = .03f;
	int count = 5;
	int max = 5;
	float dir = -1;
	enum state {posy, posx, negy, negx}
	state curstate = state.posy;
	float interval = 0;
	float timebuffer = 0;
	float cooldowninterval = 0;
	float cooldownbuffer = 0;
	bool pause = true;
	public float TransformScale;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(TransformScale,TransformScale,TransformScale);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.realtimeSinceStartup - cooldowninterval) > cooldownbuffer) 
		{
			cooldowninterval = Time.realtimeSinceStartup;
			pause = false;
		}
		
		if(pause)
		{
			return;
		}
		
		if ((Time.realtimeSinceStartup - interval) > timebuffer) 
		{
			interval = Time.realtimeSinceStartup;
			float x = transform.localScale.x;
			float y = transform.localScale.y;
			
			if(curstate == state.posx || curstate == state.negx)
			{
				//x += scale * dir;
			}
			else
			{
				y += scale * dir;
			}
			
			count -= 1;
			
			transform.localScale = new Vector3(x, y, 1f);
			
			if(count != 0)
			{
				return;
			}
			
			switch(curstate)
			{
			case(state.posy):
				curstate = state.posx;
				break;
			case(state.posx):
				dir *= -1;
				curstate = state.negy;
				break;
			case(state.negy):
				curstate = state.negx;
				break;
			case(state.negx):
				dir *= -1;
				curstate = state.posy;
				pause = true;
				break;
			}
			
			count = max;
		}
	}
}