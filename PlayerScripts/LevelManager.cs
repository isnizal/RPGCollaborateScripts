using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject player, playerCanvas;
	public string sceneToGoTo;
	public Animator transition;
	public float transitiontime;
    // Start is called before the first frame update
    void Start()
    {
		if (SceneManager.GetActiveScene().name != "TitleScreen")
		{
			//player = GameObject.FindWithTag("Player");
			//DontDestroyOnLoad(player);
			playerCanvas = GameObject.Find("WorldCanvas");
			DontDestroyOnLoad(playerCanvas);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			StartCoroutine(LoadScene());
		}
	}
	IEnumerator LoadScene()
	{
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(transitiontime);
		SceneManager.LoadScene(sceneToGoTo, LoadSceneMode.Single);
	}
	public void LoadNormalScene()
	{
		SceneManager.LoadScene(sceneToGoTo);
	}
	public void LoadSaveScene()
	{
		ProtectedSaveFiles.Basic.SaveController.Load();
		if (ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerScene == null)
		{
			Debug.Log("no save scene");
		}
		else
		{
			string toLoadScene = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerScene;
			SceneManager.LoadScene(toLoadScene);
		}
	}
}
