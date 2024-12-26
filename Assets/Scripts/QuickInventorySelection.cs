using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class QuickInventorySelection : MonoBehaviour
{
	[SerializeField] private GameObject[] inventoryImages;
	[SerializeField] private Sprite bar;
	[SerializeField] private Sprite selBar;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			removeSprites();
			inventoryImages[0].GetComponent<UnityEngine.UI.Image>().sprite = selBar;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			removeSprites();
			inventoryImages[1].GetComponent<UnityEngine.UI.Image>().sprite = selBar;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			removeSprites();
			inventoryImages[2].GetComponent<UnityEngine.UI.Image>().sprite = selBar;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			removeSprites();
			inventoryImages[3].GetComponent<UnityEngine.UI.Image>().sprite = selBar;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			removeSprites();
			inventoryImages[4].GetComponent<UnityEngine.UI.Image>().sprite = selBar;
		}
	}
	
	private void removeSprites()
	{
		foreach(GameObject image in inventoryImages)
		{
			image.GetComponent<UnityEngine.UI.Image>().sprite = bar;
		}
	}
}
