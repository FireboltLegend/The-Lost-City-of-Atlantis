using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	[SerializeField] private GameObject saveFilePrefab;
	[SerializeField, ReadOnly(true)] public GameObject selSaveFile;
	[SerializeField, ReadOnly(true)] public List<GameObject> saveFiles;
	[SerializeField] private Sprite normBar;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(player == null)
		{
			player = FindObjectOfType<PlayerController>();
		}
	}

	public void Select()
	{
		foreach(GameObject file in saveFiles)
		{
			GameObject imageGO = file.transform.GetChild(0).transform.GetChild(0).gameObject;
			imageGO.GetComponent<UnityEngine.UI.Image>().sprite = normBar;
		}
	}

	public void SaveGame()
	{
		string fileName = SaveSystem.SaveGame(player);
		GameObject saveFile = Instantiate(saveFilePrefab, transform);
		saveFile.GetComponentInChildren<TextMeshProUGUI>().text = fileName;
		saveFiles.Add(saveFile);
	}

	public void LoadGame()
	{
		if(selSaveFile != null)
		{
			PlayerData data = SaveSystem.LoadGame(selSaveFile.GetComponentInChildren<TextMeshProUGUI>().text);
			SceneManager.LoadScene("Level 0" + data.level);
			SceneManager.sceneLoaded += (scene, mode) =>
			{
				player = FindObjectOfType<PlayerController>();
				if(player != null)
				{
					player.level = data.level;
					// Debug.Log(data.position[0] + " " + data.position[1] + " " + data.position[2]);
					player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
					player.health = data.health;
					player.oxygen = data.oxygen;
				}
				else
				{
					Debug.LogError("Player not found");
				}
				SceneManager.sceneLoaded -= (scene, mode) => { }; 
			};
		}
		else
		{
			Debug.LogError("No save file selected");
		}
	}

	public void DeleteSave()
	{
		if(selSaveFile != null)
		{
			SaveSystem.DeleteSave(selSaveFile.GetComponentInChildren<TextMeshProUGUI>().text);
			saveFiles.Remove(selSaveFile);
			Destroy(selSaveFile);
		}
		else
		{
			Debug.LogError("No save file selected");
		}
	}
}
