using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
	private Player thePlayer;
	private void Start()
	{
		if (SceneManager.GetActiveScene().name == "MainWorld")
		{
			
			thePlayer = FindObjectOfType<CharacterSelection>().GetCharacterObject().GetComponent<Player>();
			if (!thePlayer.gameObject.activeInHierarchy)
			{
				thePlayer.gameObject.SetActive(true);
				thePlayer = FindObjectOfType<Player>();
				thePlayer.transform.position = GameObject.Find("FirstSpawn").transform.position;
			}

		}
		SceneManager.sceneLoaded += CheckLastSceneLoad;

	}
    private void Update()
    {
	}
	public void CheckLastSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "DungeonScene")
		{ 		
			thePlayer = FindObjectOfType<Player>();
			thePlayer.transform.position = GameObject.Find("PlayerStart").transform.position;
		}
	}

}
