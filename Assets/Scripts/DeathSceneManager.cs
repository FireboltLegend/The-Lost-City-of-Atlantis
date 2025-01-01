using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneManager : MonoBehaviour
{
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
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 1");
	}

	public void BackToMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
