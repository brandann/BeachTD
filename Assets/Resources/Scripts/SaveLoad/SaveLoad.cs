using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad
{
	public static Game SavedGame = new Game(10);
	private const string FileName = "/SavedGame.gd";
	
	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + FileName);
		bf.Serialize(file, SaveLoad.SavedGame);
		file.Close();
		//Debug.Log("Game Saved");
	}
	
	public static void Load()
	{
		if(File.Exists(Application.persistentDataPath + FileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open);
			SaveLoad.SavedGame = (Game)bf.Deserialize(file);
			file.Close();
            //Debug.Log("Game Loaded");
		}
        else
        {
            //Debug.Log("Load File Does Not Exist");
            NewGame();
        }
	}
	
	public static void Reset()
	{
		if(File.Exists(Application.persistentDataPath + FileName)) {
            SavedGame = new Game(10);
            Save();
            //Debug.Log("Game Reset");
		}
        else
        {
            //Debug.Log("reset File Does Not Exist");
            NewGame();
        }
	}

    public static void NewGame()
    {
        //Debug.Log("Creating new game...");
        SavedGame = new Game(10);
        Save();
    }
}
