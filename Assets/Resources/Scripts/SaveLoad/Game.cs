using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game 
{
    private bool[] Levels;
	public Game()
	{
        Levels = new bool[Global.MaxLevels /*MAX LEVELS*/];
        for(int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = false;
        }
        Levels[0] = true;
	}

    public void UnlockLevel(int level)
    {
    	if(level < 0 || level >= Levels.Length)
        {
            UnityEngine.Debug.LogError("Game:Level out of range");
            return;
        }

        Levels[level] = true;
    }

    public int HighestCompletedLevel()
    {
        int highest = 0;
        for(int i = 0; i < Levels.Length; i++)
        {
            if(Levels[i])
            {
                highest = i;
            }
        }

        return highest;
    }
}
