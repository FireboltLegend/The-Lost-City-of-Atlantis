using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
	public static string SaveGame(PlayerController player)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string fileName = $"player_{DateTime.Now:yyyyMMdd_HHmmss}.save";
		string path = Path.Combine(Application.persistentDataPath, fileName);
		Debug.Log($"Saving to {path}");
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();

		return fileName;
	}

	public static PlayerData LoadGame(string fileName)
	{
		string path = Path.Combine(Application.persistentDataPath, fileName);
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	public static void DeleteSave(string fileName)
	{
		string path = Path.Combine(Application.persistentDataPath, fileName);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
		}
	}
}
