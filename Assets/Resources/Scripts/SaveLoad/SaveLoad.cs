using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad
{
	public static Game SavedGame;
	private const string FileName = "/SavedGame.gd";
	
	public static void Save()
	{
		SavedGame = Game.CurrentGame;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + FileName);
		bf.Serialize(file, SaveLoad.SavedGame);
		file.Close();
	}
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + FileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open);
			SaveLoad.SavedGame = (Game)bf.Deserialize(file);
			file.Close();
		}
	}
}
