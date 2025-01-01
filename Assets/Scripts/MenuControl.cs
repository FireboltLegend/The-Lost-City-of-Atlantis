using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject loadGameMenu;

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
	}

	public void BackToMainMenu()
	{
		mainMenu.SetActive(true);
		loadGameMenu.SetActive(false);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
	}
}
