using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game 
{
	public static Game CurrentGame;
    public int CurrentLevel
    {
        get
        {
        	return _currentLevel;
        }
    }
    public void UnlockLevel(int level)
    {
    	Levels[level] = LevelStatus.Unlocked;
    	for(int i = 0; i <= level; i++)
    	{
    		Levels[i] = LevelStatus.Unlocked;
    	}
    	
		for(int i = Levels.Length - 1; i >= 0; i--)
		{
			if(Levels[i] == LevelStatus.Unlocked)
			{
				Levels[i + 1] = LevelStatus.Current;
				_currentLevel = i;
				return;
			}
		}
		return;
    }
	public enum LevelStatus{Unlocked, Current, Locked}
	public LevelStatus[] Levels = new LevelStatus[Global.MaxLevels];
	private int _currentLevel;
}
