using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSytem : MonoBehaviour
{
    //reference for inventory equipment to add item after buy
    public InventoryObject playerInventory;
    //reference for item database to get the item object stats
    public ItemDatabaseObject itemDatabaseItemObj;
    //compare to the game object item ground name
    public GroundItem[] groundItem;
    //show pop up screen to buy
    public GameObject confirmationBuyScreen;
    //show screen sell
    public GameObject confirmationSellScreen;
    //deactivate buy button
    public GameObject buyButton;

    //reference item button
    public Button weaponButton, swordButton, axeButton, wandButton, hammerButton, bowButton, arrowButton, daggerButton, torsoButton, helmetButton, shieldButton, bootButton, healingButton, foodButton, potionButton;
    //reference item drop
    public TMP_Dropdown swordDropDown, axeDropDown, bowDropDown, hammerDropDown, arrowDropDown, daggerDropDown, wandDropDown, torsoDropDown, helmetDropDown, shieldDropDown, bootDropDown, potionDropDown, foodDropDown;
    //reference tool tip
    public GameObject swordToolTip, torsoToolTip, helmetToolTip, shieldToolTip, bootToolTip, healingToolTip;
    //reference shop
    public GameObject shopGameObject;
    //reference text mesh pro for sword drop down name, item level, rarity, value
    public TextMeshProUGUI t_swordName, t_swordItemLevel, t_swordRarity, t_swordValue;
    //reference text mesh pro for helmet drop down name, item level, rarity, value
    public TextMeshProUGUI t_helmetName, t_helmetItemLevel, t_helmetRarity, t_helmetValue;
    //reference text mesh pro for torso drop down name, item level, rarity, value
    public TextMeshProUGUI t_torsoName, t_torsoItemLevel, t_torsoRarity, t_torsoValue;
    //reference text mesh pro for shield drop down name, item level, rarity, value
    public TextMeshProUGUI t_shieldName, t_shieldItemLevel, t_shieldRarity, t_shieldValue;
    //reference text mesh pro for boot  drop down name, item level, rarity, value
    public TextMeshProUGUI t_bootName, t_bootItemLevel, t_bootRarity, t_bootValue;
    //reference text mesh pro for healing drop down name, item level, rarity, value
    public TextMeshProUGUI t_healingName, t_healingItemLevel, t_healingRarity, t_healingAmount;
    //reference for the item price
    public TextMeshProUGUI t_itemPrice;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //function sword button press
    public void SwordButton()
    {
        //deactivate all weapon drop down
        axeDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //drop down activate
        swordDropDown.gameObject.SetActive(true);
    }
    //function axe button press
    public void AxeButton()
    {
        swordDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //axe drop down activate
        axeDropDown.gameObject.SetActive(true);
    }
    //function bow button press
    public void BowButton()
    {
        //deactivate all drop down
        axeDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        //activate bow drop down
        bowDropDown.gameObject.SetActive(true);
    }
    //function hammer button press
    //deactivate all drop down
    //activate hammer drop down
    public void HammerButton()
    {
        //deactivate all drop down
        axeDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //activate bow drop down
        hammerDropDown.gameObject.SetActive(true);
    }
    //function dagger button press
    public void DaggerButton()
    {
        //deactivate all drop down
        axeDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //activate bow drop down
        daggerDropDown.gameObject.SetActive(true);
    }
    //function wand button
    public void WandButton()
    {
        //deactivate all drop down
        axeDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        arrowDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //activate bow drop down
        wandDropDown.gameObject.SetActive(true);
    }
    //function bolt arrow button
    public void BoltButton()
    {
        //deactivate all drop down
        axeDropDown.gameObject.SetActive(false);
        hammerDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        wandDropDown.gameObject.SetActive(false);
        daggerDropDown.gameObject.SetActive(false);
        bowDropDown.gameObject.SetActive(false);
        //activate bow drop down
        arrowDropDown.gameObject.SetActive(true);
    }

    //function player activate shop
    public void ActivateShop()
    {

        // Debug.Log(swordDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString());

        //enable the game object activate to true
        shopGameObject.gameObject.SetActive(true);
    }


    //function player deactivate shop
    public void DeactivateShop()
    {
        //disable game object to false
        shopGameObject.SetActive(false);
    }
    //reference store gameobject name to buy
    private string selectedItem;
    //store the selected object to add to inventory system
    private GroundItem selectedObject;
    //function weapon button press
    public void WeaponButtonPress()
    {
        //deactivate torso button, helmet, shield and boots
        //deactivate healing drop down gameobject
        torsoButton.gameObject.SetActive(false);
        helmetButton.gameObject.SetActive(false);
        shieldButton.gameObject.SetActive(false);
        bootButton.gameObject.SetActive(false);
        potionDropDown.gameObject.SetActive(false);
        foodDropDown.gameObject.SetActive(false);
        //deactivate torso tool tip, helmet, shield and boot
        torsoToolTip.gameObject.SetActive(false);
        helmetToolTip.gameObject.SetActive(false);
        shieldToolTip.gameObject.SetActive(false);
        bootToolTip.gameObject.SetActive(false);
        //deactivate healing tool tip
        healingToolTip.gameObject.SetActive(false);
        //activate sword drop game object
        swordButton.gameObject.SetActive(true);
        axeButton.gameObject.SetActive(true);
        wandButton.gameObject.SetActive(true);
        hammerButton.gameObject.SetActive(true);
        arrowButton.gameObject.SetActive(true);
        bowButton.gameObject.SetActive(true);
        daggerButton.gameObject.SetActive(true);
        potionButton.gameObject.SetActive(false);
        foodButton.gameObject.SetActive(false);

    }


    //function armor button press
    public void ArmorButtonPress()
    {
        //activate helmet button, torso, shield and boot
        helmetButton.gameObject.SetActive(true);
        torsoButton.gameObject.SetActive(true);
        shieldButton.gameObject.SetActive(true);
        bootButton.gameObject.SetActive(true);
        //deactivate healing drop down and sword drop down
        potionDropDown.gameObject.SetActive(false);
        foodDropDown.gameObject.SetActive(false);
        swordDropDown.gameObject.SetActive(false);
        //deactivate healing and dword tool tip
        healingToolTip.gameObject.SetActive(false);
        swordToolTip.gameObject.SetActive(false);
        swordButton.gameObject.SetActive(false);
        axeButton.gameObject.SetActive(false);
        wandButton.gameObject.SetActive(false);
        hammerButton.gameObject.SetActive(false);
        arrowButton.gameObject.SetActive(false);
        bowButton.gameObject.SetActive(false);
        daggerButton.gameObject.SetActive(false);
        potionButton.gameObject.SetActive(false);
        foodButton.gameObject.SetActive(false);

    }

    // small function for the helmet get reference button for helmet
    public void GetHelmetDatabase() {
        //activate drop down for the helmet
        helmetDropDown.gameObject.SetActive(true);
        //deactivate drop down for the torso, shield, boot, their tool tip too
        torsoDropDown.gameObject.SetActive(false);
        shieldDropDown.gameObject.SetActive(false);
        bootDropDown.gameObject.SetActive(false);

        torsoToolTip.gameObject.SetActive(false);
        shieldToolTip.gameObject.SetActive(false);
        bootToolTip.gameObject.SetActive(false);
        //deactivate drop down for the sword and healing
        swordDropDown.gameObject.SetActive(false);
        potionDropDown.gameObject.SetActive(false);
        //deactivate their tool tip
        swordToolTip.gameObject.SetActive(false);
        healingToolTip.gameObject.SetActive(false);

    }
    //small function for the torso to activate the drop down
    public void GetTorsoDatabase()
    {
        //deactivate other drop down helmet, boot, and shield,their tool tip too
        helmetDropDown.gameObject.SetActive(false);
        bootDropDown.gameObject.SetActive(false);
        shieldDropDown.gameObject.SetActive(false);

        helmetToolTip.gameObject.SetActive(false);
        bootToolTip.gameObject.SetActive(false);
        shieldToolTip.gameObject.SetActive(false);
        //deactivate drop down for the sword and healing
        swordDropDown.gameObject.SetActive(false);
        potionDropDown.gameObject.SetActive(false);
        //deactivate their tool tip
        swordToolTip.gameObject.SetActive(false);
        healingToolTip.gameObject.SetActive(false);
        //activate torso 
        torsoDropDown.gameObject.SetActive(true);

    }
    //small function for the boots to activate their drop down item
    public void GetBootDatabase()
    {
        //activate boot drop down
        bootDropDown.gameObject.SetActive(true);
        //deactivate other drop down helmet, torso and shield, their tool tip too
        helmetDropDown.gameObject.SetActive(false);
        torsoDropDown.gameObject.SetActive(false);
        shieldDropDown.gameObject.SetActive(false);

        helmetToolTip.gameObject.SetActive(false);
        torsoToolTip.gameObject.SetActive(false);
        shieldToolTip.gameObject.SetActive(false);
        //deactivate drop down for the sword and healing
        swordDropDown.gameObject.SetActive(false);
        potionDropDown.gameObject.SetActive(false);
        //deactivate their tool tip
        swordToolTip.gameObject.SetActive(false);
        healingToolTip.gameObject.SetActive(false);

    }
    //small function for the shield to activate drop down list
    public void GetShieldDatabase()
    {
        //activate shield drop down
        shieldDropDown.gameObject.SetActive(true);
        //deactivate other drop down for the helmet, torso, and boots, their tool tip too
        helmetDropDown.gameObject.SetActive(false);
        torsoDropDown.gameObject.SetActive(false);
        bootDropDown.gameObject.SetActive(false);

        helmetToolTip.gameObject.SetActive(false);
        torsoToolTip.gameObject.SetActive(false);
        bootToolTip.gameObject.SetActive(false);
        //deactivate drop down for the sword and healing
        swordDropDown.gameObject.SetActive(false);
        potionDropDown.gameObject.SetActive(false);
        //deactivate their tool tip
        swordToolTip.gameObject.SetActive(false);
        healingToolTip.gameObject.SetActive(false);

    }
    //function healing button press
    public void HealingButtonPress()
    {
        swordButton.gameObject.SetActive(false);
        axeButton.gameObject.SetActive(false);
        wandButton.gameObject.SetActive(false);
        hammerButton.gameObject.SetActive(false);
        arrowButton.gameObject.SetActive(false);
        bowButton.gameObject.SetActive(false);
        daggerButton.gameObject.SetActive(false);
        //armor button set active to false
        helmetButton.gameObject.SetActive(false);
        torsoButton.gameObject.SetActive(false);
        shieldButton.gameObject.SetActive(false);
        bootButton.gameObject.SetActive(false);

        //other button press deactivate armor drop and sword drop deactivate their tool tip too
        helmetDropDown.gameObject.SetActive(false);
        torsoDropDown.gameObject.SetActive(false);
        shieldDropDown.gameObject.SetActive(false);
        bootDropDown.gameObject.SetActive(false);
        swordDropDown.gameObject.SetActive(false);
        foodButton.gameObject.SetActive(true);
        potionButton.gameObject.SetActive(true);
        //deactivate armor tooltip
        helmetToolTip.gameObject.SetActive(false);
        torsoToolTip.gameObject.SetActive(false);
        shieldToolTip.gameObject.SetActive(false);
        bootToolTip.gameObject.SetActive(false);
        //deactivate sword tool tip
        swordToolTip.gameObject.SetActive(false);

    }
    public void PotionButtonPress() {
        potionDropDown.gameObject.SetActive(true);
        foodDropDown.gameObject.SetActive(false);
    }
    public void FoodButtonPress() {
        foodDropDown.gameObject.SetActive(true);
        potionDropDown.gameObject.SetActive(false);
    }
    public void ActivateDropDownSword()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = swordDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
       Debug.Log(selectedItem);
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {

            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                Debug.Log(itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name);
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownAxe()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = axeDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        Debug.Log(selectedItem);
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {

            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                Debug.Log(itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name);
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownHammer()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = hammerDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {

            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownDagger()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = daggerDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {

            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownWand()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = wandDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownArrow()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = arrowDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        //Debug.Log(selectedItem);
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {

            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //Debug.Log(itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name);
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity;
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values;
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        //FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownBow()
    {
        //get component drop down of the option string type
        //store the string type to current of the press for the player to confirm
        selectedItem = bowDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        //add tool tip
        swordToolTip.gameObject.SetActive(true);
        //find text children name text, item level, rarity, and value
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_swordName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_swordItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_swordRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_swordValue.text = "Item Damage: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
                break;
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownHelmet() {
        //get reference option drop down type string
        selectedItem = helmetDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //store the string type to current press for the player to confirm buy
        //add tool tip
        helmetToolTip.gameObject.SetActive(true);
        //component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_helmetName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_helmetItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_helmetRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_helmetValue.text = "Item Defense: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownTorso()
    {        //store the string type to confirm buy  to the player
        selectedItem = torsoDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //add tool tip
        torsoToolTip.gameObject.SetActive(true);
        //component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_torsoName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_torsoItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_torsoRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_torsoValue.text = "Item Defense: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownShield()
    {         //store the string option of the item
        selectedItem = shieldDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //add tool tip for the player
        shieldToolTip.gameObject.SetActive(true);
        //get their component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_shieldName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_shieldItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_shieldRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_shieldValue.text = "Item Defense: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownBoot()
    {        //store the string type to current press for the player to confirm buy
        selectedItem = bootDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //add tool tip
        bootToolTip.gameObject.SetActive(true);
        //component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_bootName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_bootItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_bootRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_bootValue.text = "Item Defense: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().values.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }
    public void ActivateDropDownFood()
    {        //get reference drop down for the healing
        selectedItem = foodDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //activate drop down button press
        healingToolTip.gameObject.SetActive(true);
        //store reference for the drop down string for the player confirm buy
        //add tool tip for the player to see 
        //get their component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_healingName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_healingItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_healingRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_healingAmount.text = "Item Amount: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().consumables.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem(); 
    }
    public void ActivateDropDownPotion()
    {        //get reference drop down for the healing
        selectedItem = potionDropDown.GetComponent<TMP_Dropdown>().captionText.text.ToString();
        FindItemFromArrayOfGroundItem();
        //activate drop down button press
        healingToolTip.gameObject.SetActive(true);
        //store reference for the drop down string for the player confirm buy
        //add tool tip for the player to see 
        //get their component text to show
        for (int itemDatabase = 0; itemDatabase < itemDatabaseItemObj.ItemObjects.Length; itemDatabase++)
        {
            if (selectedItem == itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name)
            {
                //set the text to the selected item property
                t_healingName.text = "Name: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.Name.ToString();
                t_healingItemLevel.text = "Item Level: " + itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemLevel.ToString();
                t_healingRarity.text = "Item Rarity: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().itemRarity.ToString();
                t_healingAmount.text = "Item Value: " + itemDatabaseItemObj.ItemObjects[itemDatabase].CreateItem().consumables.ToString();
                t_itemPrice.text = itemDatabaseItemObj.ItemObjects[itemDatabase].newItem.itemPrice.ToString();
            }
            else
            {
                Debug.Log("item not in database or there is no selected item to buy");
            }
        }
        FindItemFromArrayOfGroundItem();
    }

    public GameObject fullSlot;
    public GameObject enableWindowNoMoney;
    Player player;
    //function confirm buy Item
    public void ConfirmBuy() {
        // get the store of the reference string of item
        if (selectedObject != null)
        {
            player = FindObjectOfType<Player>();
            //get the price of the item
            int itemPrice = selectedObject.itemObject.newItem.itemPrice;
            //minus the gold of the item
            //money more than zero to minus
            if (player.currentGold > 0)
            {
                Debug.Log("have money");
                player.currentGold -= itemPrice;
                DeactivateAllToolTip();
                B_confirm();
                if (player.currentGold <= 0)
                {
                    player.currentGold = 0;
                }
            }
            //no money window deactivate all tool tip
            else
            {
                Debug.Log("no money");
                enableWindowNoMoney.SetActive(true);
                DeactivateAllToolTip();
                B_Nconfirm();
                buyButton.SetActive(false);
            }
            //check for inventory slot is full
            if (playerInventory.EmptySlotCount <= 0)
            {
                //pop out small window saying inventory is full please remove some item
                //add back gold to the player
                player.currentGold += itemPrice;
                fullSlot.SetActive(true);
            }
            //check full slot is active deactivate tool tip and set full slot to deactiv
            if(fullSlot.activeInHierarchy)
            {
                DeactivateAllToolTip();
                fullSlot.SetActive(false);
            }
        }
        else
        {
            //else to return null said no item selected to buy can exit
            Debug.Log("no item selecte");
        }

    }
    //deactivate all tool tip
    public void DeactivateAllToolTip()
    {
        swordToolTip.SetActive(false);
        helmetToolTip.SetActive(false);
        torsoToolTip.SetActive(false);
        shieldToolTip.SetActive(false);
        bootToolTip.SetActive(false);
        healingToolTip.SetActive(false);
    }
    //add item to inventory
    public void AddItemInventory()
    {
        Item newItem = new Item(selectedObject.itemObject);
        //else to add item to inventory
        player.inventory.AddItem(newItem,1);
        fullSlot.SetActive(false);
        buyButton.SetActive(false);
        B_Nconfirm();
    }
    //variable selected obj to sell
    private InventorySlot selectedObjToSell;
    //function to get the selected obj to sell
    public InventorySlot GetSelectedObjToSell(InventorySlot mouseHoverSelected)
    {
        selectedObjToSell = mouseHoverSelected;
        ConfirmSellItem();
        //function confirm sell item
        return selectedObjToSell;
    }
    public void ConfirmSellItem()
    {
        //check  selected object not null
        if (selectedObjToSell != null)
        {
            //small window pop up  said want to sell
            confirmationSellScreen.gameObject.SetActive(true);
        }
    }
    //function yes sell button
    public void YesSell()
    {
        //get selected obj from inventory on the mouse when drag
        if (selectedObjToSell != null)
        {
           // Debug.Log(selectedObjToSell.ItemObject);
            //get the item price to divide by 1/3
            float objPrice = selectedObjToSell.ItemObject.newItem.itemPrice;
            //Debug.Log(objPrice);
            objPrice *= 0.3f;
            //sum the current price with current gold
            //Debug.Log(objPrice);
            FindObjectOfType<Player>().currentGold += Mathf.RoundToInt( objPrice);
            //Debug.Log(FindObjectOfType<Player>().currentGold);
            confirmationSellScreen.gameObject.SetActive(false);
            selectedObjToSell.RemoveItem();
        }
    }

    //function disable pop up screen buy item
    public void B_Nconfirm()
    {
        //disable the pop up screen
        confirmationBuyScreen.SetActive(false);
    }
    //function enable pop up screen to buy
    public void B_confirm()
    {
        //enable pop up screen to confirm buy
        confirmationBuyScreen.SetActive(true);
        
    }
    //reference control loop to prevent error looping buying the same item
    private bool controlLoop;
    //referece item can buy
    private bool itemEnable;
    //function for loop one call after player trully buy
    void FindItemFromArrayOfGroundItem()
    {
        // loop through player ground item
        for (int currentGroundItem = 0; currentGroundItem < groundItem.Length; currentGroundItem++)
        {
            //check for selected item found in the ground item else return not found
            if (selectedItem == groundItem[currentGroundItem].itemObject.newItem.Name)
            {
                //store the ground item game object to add the item to reference object to buy
                selectedObject = groundItem[currentGroundItem].gameObject.GetComponent<GroundItem>();
            }
        }
        buyButton.SetActive(true);
    }
}
