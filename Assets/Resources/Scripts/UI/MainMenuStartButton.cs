using UnityEngine;
using System.Collections;

public class MainMenuStartButton : SlidingUI{

	public MainMenuCreditsButton Credits;
    public AnimationClip SlideAnimation;    
    
    private float _slideTime;    

    public override void Start()
    {
        base.Start();        
        _slideTime = SlideAnimation.length;
    }

	public override void Clicked ()
	{
		base.Clicked ();
		//Credits.Slide ();
        //StartCoroutine(LoadLevel()); //Todo swap these out for release
        //SaveLoad s = new SaveLoad();
        SaveLoad.Load();
        Game g = SaveLoad.SavedGame;
		int index = 0;
		if(g != null)
        {
			for(int i = 0; i < Global.MaxLevels; i++)
			{
				if(g.Levels[i] == Game.LevelStatus.Current) { index = i;}
			}
		}
        GameObject.Find("Global").GetComponent<Global>().LoadMap(index);
        Application.LoadLevel(Global.Scenes.Game.ToString()); 
	}

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(_slideTime);
		//Application.LoadLevel(Global.Scenes.Levels.ToString());     
    }
	
}
