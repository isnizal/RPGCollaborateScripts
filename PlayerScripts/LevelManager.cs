using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject player, playerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        DontDestroyOnLoad(player);
        playerCanvas = GameObject.Find("WorldCanvas");
        DontDestroyOnLoad(playerCanvas);
        //StartCoroutine(WaitForSecond());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(3f);
        LoadScene("DungeonScene");
    }
}
