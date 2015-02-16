﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImmersiveModeEnabler : MonoBehaviour {

	AndroidJavaObject unityActivity;
	AndroidJavaObject javaObj;
	AndroidJavaClass javaClass;
    public Text text;

	bool paused;
	static bool created;


	void Awake()
	{
        text.text = "Awake";

		if(!Application.isEditor)
			HideNavigationBar();
		if(!created)
		{
			DontDestroyOnLoad(gameObject);
			created = true;
		}
		else
		{
			Destroy(gameObject); // duplicate will be destroyed if 'first' scene is reloaded
		}
	}
	
	void HideNavigationBar()
	{
        text.text = "hide bar";

		#if UNITY_ANDROID
        text.text = "Android";
		lock(this)
		{
            text.text = "Inside lock";
			using(javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			
			if(unityActivity == null)
			{
                text.text = "First return";
				return;                
			}
			
			using(javaClass = new AndroidJavaClass("com.rak24.androidimmersivemode.Main"))
			{
				if(javaClass == null)
				{
                    text.text = "Second return";
					return;                    
				}
				else
				{
					javaObj = javaClass.CallStatic<AndroidJavaObject>("instance");
                    text.text = "created obj";
                    if (javaObj == null)
                    {
                        text.text = "Third return";
                        return;
                    }
                    text.text = "About to call";
					unityActivity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
					                                                           {
						javaObj.Call("EnableImmersiveMode", unityActivity);
					}));
                    text.text = "Java called";
				}
			}
		}
		#endif
	}
	
	void OnApplicationPause(bool pausedState)
	{
		paused = pausedState;
	}
	
	void OnApplicationFocus(bool hasFocus)
	{
		if(hasFocus)
		{
			if(javaObj != null && paused != true)
			{
				unityActivity.Call("runOnUiThread",new AndroidJavaRunnable(() => 
						                                                           {
							javaObj.CallStatic("ImmersiveModeFromCache", unityActivity);
						}));
			}
		}
		
	}
	
	public void PinThisApp() // Above android 5.0 - App Pinning
	{
		if(javaObj != null)
		{
			javaObj.CallStatic("EnableAppPin",unityActivity);
		}
	}
	
	public void UnPinThisApp() // Unpin the app
	{
		if(javaObj != null)
		{
			javaObj.CallStatic("DisableAppPin",unityActivity);
		}
	}

}
