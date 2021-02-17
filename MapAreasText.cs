using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapAreasText : MonoBehaviour
{
    public bool needText;
    public string mapName;
    public GameObject text;
    public TextMeshProUGUI mapText;

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
		text.SetActive(true);
		mapText.text = mapName;
		yield return null;
	}
}
