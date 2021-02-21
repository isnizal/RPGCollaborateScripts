using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player UI Window")]
    public Slider healthBar;
    public Slider manaBar;
    public Slider xPBar;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI strValueText;
    public TextMeshProUGUI defValueText;
    public TextMeshProUGUI intValueText;
    public TextMeshProUGUI statsPointsText;

    [Header("Drop Window Objects")]
    public TextMeshProUGUI dropItemName;
    public TextMeshProUGUI dropItemText;
    
    private Player player;


    [Header("UI Windows")]
    public GameObject inventoryWindow;
    public GameObject equipmentWindow;
    public GameObject statsWindow;

    public GameObject DropUI;

    [HideInInspector]
    public UserInterface userInterface;
    //create gameobject of the item to drop on  the field variable ground item based on string name
    public GroundItem[] groundItem ;

    void Start()
    {
        player = FindObjectOfType<Player>();
        userInterface = FindObjectOfType<UserInterface>();
        //DontDestroyOnLoad(this.gameObject);

    }


    void Update()
    {
        healthBar.maxValue = player.playerMaxHP;
        healthBar.value = player.playerCurrentHP;
        manaBar.maxValue = player.playerMaxMana;
        manaBar.value = player.playerCurrentMana;

        xPBar.minValue = player.LevelSystem.GetXPForLevel(player.LevelSystem.currentLevel - 1);
        xPBar.maxValue = player.LevelSystem.GetXPForLevel(player.LevelSystem.currentLevel + 1);
        xPBar.value = player.LevelSystem.experience;

        levelText.text = "" + player.LevelSystem.currentLevel;

        XPText.text = "" + player.LevelSystem.experience + "/" + player.LevelSystem.GetXPForLevel(player.LevelSystem.currentLevel + 1);

        strValueText.text = "" + player.playerAttackPower.ToString();
        defValueText.text = "" + player.playerDefensePower.ToString();
        intValueText.text = "" + player.playerIntelligencePower.ToString();

        statsPointsText.text = "" + player.statPoints;
    }

    public void InventoryWindow(CanvasGroup canvas)
	{
        if (canvas.alpha == 0)
            inventoryWindow.GetComponent<CanvasGroup>().alpha = 1;
        else
            inventoryWindow.GetComponent<CanvasGroup>().alpha = 0;
	}
    public void EquipmentWindow()
	{
        if (!equipmentWindow.activeInHierarchy)
            equipmentWindow.SetActive(true);
        else
            equipmentWindow.SetActive(false);
    }
    public void StatsWindow()
    {
        if (!statsWindow.activeInHierarchy)
            statsWindow.SetActive(true);
        else
            statsWindow.SetActive(false);
    }
    //control spawn at the screen, to spawn only once
    [HideInInspector]
    public bool controlSpawn;
    public void ConfirmYesDrop(string nameItem, Vector2 lastMousePos,InventorySlot slot)
    {
        controlSpawn = true;
        //controlling spawn at the wall
        //if (dontSpawnAtWall)
        //{
        //loop through all of the object to instantiate
        for (int item = 0; item < groundItem.Length; item++)
        {
            //instantiate the item object based on the string name parameter at the last mouse current position
            if (nameItem == groundItem[item].itemObject.newItem.Name)
            {
                //dontSpawnAtWall = true;
                //check the item type is food
                if (slot.ItemObject.itemType == ItemType.Healing)
                {

                    Debug.Log("heal item");
                    //instant food
                    if (controlSpawn)
                    {
                        GameObject itemObj = Instantiate(groundItem[item].gameObject, new Vector2(lastMousePos.x, lastMousePos.y), Quaternion.identity) as GameObject;
                        itemObj.transform.SetParent(GameObject.Find("ParentItemDrop").transform.parent);
                        //minus the amount have of food
                        slot.newAmount -= 1;
                        //updated the slot interface
                        slot.UpdateSlot(slot.newItem, slot.newAmount);
                        //remove the item amount is zero
                        if (slot.newAmount <= 0)
                            slot.RemoveItem();
                        //typeOf = null;
                        //nameItem = null;
                        controlSpawn = false;
                        //break;
                    }
                }
                else if (slot.ItemObject.itemType != ItemType.Healing)
                {
                    Debug.Log("not heal item");
                    if (controlSpawn)
                    {
                        GameObject itemObj = Instantiate(groundItem[item].gameObject, new Vector2(lastMousePos.x, lastMousePos.y), Quaternion.identity) as GameObject;
                        itemObj.transform.SetParent(GameObject.Find("ParentItemDrop").transform.parent);
                        //remove the item after spawn
                        slot.RemoveItem();
                        //typeOf = null;
                        controlSpawn = false;

                    }
                }
                else
                {
                    Debug.Log("no item type");
                }
                if (DropUI.activeInHierarchy)
                    DropUI.SetActive(false);
            }

        } 
            controlSpawn = true;
        //}

    }
    public void ConfirmNoDrop()
    {
        if (DropUI.activeInHierarchy)
            DropUI.SetActive(false);

        dontSpawnAtWall = true;
    }
    //refere the bool dontSpawnAtWall
    [HideInInspector]
    public bool dontSpawnAtWall = true;
    //function get controll spawn
    public bool returnControlSpawn() { return dontSpawnAtWall; }
}
