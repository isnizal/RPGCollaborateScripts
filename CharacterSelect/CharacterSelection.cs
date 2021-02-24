using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	[SerializeField]
	private GameObject warriorPrefab;
	[SerializeField]
	private GameObject archerPrefab;
	[SerializeField]
	private GameObject magePrefab;
	[SerializeField]
	private GameObject characterSelection;

    private void Start()
    {
		DontDestroyOnLoad(gameObject);
	}
	GameObject characterObject;
    public void ConfirmWarrior(string character)
	{
		switch (character)
		{
			case "warriorPrefab":
				characterObject = Instantiate(warriorPrefab);
				characterObject.transform.SetParent(transform);
				characterObject.gameObject.SetActive(false);
				break;
			case "magePrefab":
				characterObject = Instantiate(warriorPrefab);
				characterObject.transform.SetParent(transform);
				characterObject.gameObject.SetActive(false);
				break;
			case "archerPrefab":
				characterObject = Instantiate(warriorPrefab);
				characterObject.transform.SetParent(transform);
				characterObject.gameObject.SetActive(false);
				break;
		}
		characterSelection.gameObject.SetActive(false);
		SceneManager.LoadScene("MainWorld");
		
	}
	public GameObject GetCharacterObject()
	{
		return characterObject;
	}
}
