using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //get ui manager
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        //find ui manager
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //controlling
    private void OnMouseEnter()
    {

    }
    private void OnMouseOver()
    {

        //set dontspawnatwall to false
        uiManager.dontSpawnAtWall = false;
    }
    private void OnMouseExit()
    {

    }

}
