using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private int nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += (scene, mode) =>
		{
			PlayerController player = FindObjectOfType<PlayerController>();
			if(player != null)
			{
				player.level = nextLevel;
				// Debug.Log(data.position[0] + " " + data.position[1] + " " + data.position[2]);
				player.transform.position = startPosition;
				player.health = 100;
				player.oxygen = 100;
			}
			else
			{
				Debug.LogError("Level Couldn't be Loaded");
			}
			SceneManager.sceneLoaded -= (scene, mode) => { }; 
		};
    }
}
