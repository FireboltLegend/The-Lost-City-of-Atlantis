using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DeathSceneManager : MonoBehaviour
{
	[SerializeField] private GameObject deathGameMenu;
	[SerializeField] private GameObject loadGameMenu;
	[SerializeField] private GameObject saveFilePrefab;
	[SerializeField] private LoadGameManager loadGameManager;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void LoadGame()
	{
		deathGameMenu.SetActive(false);
		loadGameMenu.SetActive(true);
		foreach(GameObject file in loadGameManager.saveFiles)
		{
			Destroy(file);
		}
		loadGameManager.saveFiles.Clear();
		string[] saveFiles = GetAllSaveFiles();
		foreach(string file in saveFiles)
		{
			GameObject saveFile = Instantiate(saveFilePrefab, loadGameManager.transform);
			saveFile.GetComponentInChildren<TextMeshProUGUI>().text = file;
			loadGameManager.saveFiles.Add(saveFile);
		}
	}

	public static string[] GetAllSaveFiles()
	{
		string path = Application.persistentDataPath;
		if (Directory.Exists(path))
		{
			return Directory.GetFiles(path, "*.save");
		}
		else
		{
			Debug.LogError("Save directory not found: " + path);
			return new string[0];
		}
	}

	public void BackToMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
