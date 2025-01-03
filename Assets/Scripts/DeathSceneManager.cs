using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneManager : MonoBehaviour
{
	[SerializeField] private GameObject deathGameMenu;
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
		deathGameMenu.SetActive(false);
		loadGameMenu.SetActive(true);
	}

	public void BackToMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
