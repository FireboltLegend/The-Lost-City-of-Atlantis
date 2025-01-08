using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject loadGameMenu;
	[SerializeField] private GameObject controlMenu;
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
		mainMenu.SetActive(false);
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
		mainMenu.SetActive(true);
		loadGameMenu.SetActive(false);
		controlMenu.SetActive(false);
	}

	public void Controls()
	{
		mainMenu.SetActive(false);
		controlMenu.SetActive(true);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level 01");
	}

	public void LoadMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
