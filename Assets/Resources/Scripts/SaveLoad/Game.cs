using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game 
{
	public static Game CurrentGame;
	public enum LevelStatus{Unlocked, Current, Locked}
	public LevelStatus[] Levels = new LevelStatus[25];
}
