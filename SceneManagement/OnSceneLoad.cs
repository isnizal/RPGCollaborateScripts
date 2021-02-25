using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
	private Player thePlayer;
	[SerializeField]
	private GameObject createCharacterSelection;
	private bool characterSelect;
	private void Start()
	{
		findCharacterSelection();
    }
	void findCharacterSelection()
	{
		bool character = FindObjectOfType<CharacterSelection>();
		if (!character)
		{
             //create character selection with warrior
             ProtectedSaveFiles.Basic.SaveController.Load();
             string currentCharacterSelection = ProtectedSaveFiles.Basic.SaveController.Data.SaveCharacterSelection;
             var characSelect = Instantiate(createCharacterSelection);
             characSelect.GetComponent<CharacterSelection>().ConfirmWarrior(currentCharacterSelection);
             thePlayer = characSelect.GetComponent<CharacterSelection>().GetCharacterObject().GetComponent<Player>();
             thePlayer.gameObject.SetActive(true);
             if (thePlayer != null)
             {
                 //load the player profile
                 thePlayer.playerCurrentHP = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerHP;
                 thePlayer.playerCurrentMana = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerMana;
                 thePlayer.currentGold = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerGold;
                 thePlayer.playerExperience = ProtectedSaveFiles.Basic.SaveController.Data.SaveCurrentEXP;
                 thePlayer.playerPositionX = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationX;
                 thePlayer.playerPositionY = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationY;

                 thePlayer.transform.position = new Vector2(thePlayer.playerPositionX, thePlayer.playerPositionY);
                 return;
             }
        }
		else
		{
           thePlayer = FindObjectOfType<CharacterSelection>().GetCharacterObject().GetComponent<Player>();
           if (!thePlayer.gameObject.activeInHierarchy)
           {
               thePlayer.gameObject.SetActive(true);
               thePlayer = FindObjectOfType<Player>();
               thePlayer.transform.position = GameObject.Find("FirstSpawn").transform.position;
           }
           else
           {
               SceneManager.sceneLoaded += CheckLastSceneLoad;
           }
		}
	}
    private void Update()
    {
	}
	public void CheckLastSceneLoad(Scene scene, LoadSceneMode mode)
	{
		
			thePlayer = FindObjectOfType<Player>();
			thePlayer.transform.position = GameObject.Find("PlayerStart").transform.position;
	}

}
