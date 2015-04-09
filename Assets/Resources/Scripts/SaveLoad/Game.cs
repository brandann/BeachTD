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

        set
        {
            if(value < 0 || value > Global.MaxLevels){
                Debug.LogError("Invalid current level");
                return;
            }

            for (int i = 0; i < value; i++)
                Levels[i] = LevelStatus.Unlocked;

            Levels[value] = Game.LevelStatus.Current;
            _currentLevel = value;

            for(int i = _currentLevel + 1; i < Global.MaxLevels; ++i)
                Levels[i] = LevelStatus.Locked;  
        }
        
    }
	public enum LevelStatus{Unlocked, Current, Locked}
	public LevelStatus[] Levels = new LevelStatus[Global.MaxLevels];

    private int _currentLevel;
}
