using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapAreasText : MonoBehaviour
{
    public bool needText;
    public string mapName;
    public GameObject textDisplay;
    public TextMeshProUGUI mapText;

	private void Start()
	{
		AttachTextDisplay();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
			{
			if(needText)
			{
				StartCoroutine(mapNameCo());
			}
		}
	}

	private IEnumerator mapNameCo()
	{
		textDisplay.SetActive(true);
		mapText.text = mapName;
		yield return null;
	}
	void AttachTextDisplay()
	{
		if (textDisplay == null && mapText == null)
		{
			textDisplay = GameObject.Find("MapAreaTitle");
			mapText = GameObject.Find("MapText").GetComponent<TextMeshProUGUI>();
		}
	}
}
