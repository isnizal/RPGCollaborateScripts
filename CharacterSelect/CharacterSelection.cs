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
	private GameObject characterSelectionPanel;

	public string saveCharacterName;

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
				saveCharacterName = character;
				characterObject.gameObject.SetActive(false);
				break;
			case "magePrefab":
				characterObject = Instantiate(magePrefab);
				characterObject.transform.SetParent(transform);
				saveCharacterName = character;
				characterObject.gameObject.SetActive(false);
				break;
			case "archerPrefab":
				characterObject = Instantiate(archerPrefab);
				characterObject.transform.SetParent(transform);
				saveCharacterName = character;
				characterObject.gameObject.SetActive(false);
				break;
		}
		characterSelectionPanel.gameObject.SetActive(false);
		if (SceneManager.GetActiveScene().name == "CharacterSelect")
		{
			SceneManager.LoadScene("MainWorld");
		}
	}
	public GameObject GetCharacterObject()
	{
		return characterObject;
	}
}
