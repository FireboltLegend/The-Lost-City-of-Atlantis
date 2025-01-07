using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
	[SerializeField] private Sprite selBar;
	[SerializeField] private GameObject imageGO;
	[SerializeField, ReadOnly(true)] private LoadGameManager loadGameManager;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(loadGameManager == null)
		{
			loadGameManager = FindObjectOfType<LoadGameManager>();
		}
	}

	public void Select()
	{
		loadGameManager.Select();
		loadGameManager.selSaveFile = gameObject;
		imageGO.GetComponent<UnityEngine.UI.Image>().sprite = selBar;
	}
}
