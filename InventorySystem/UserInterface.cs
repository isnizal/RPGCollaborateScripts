using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public abstract class UserInterface : MonoBehaviour
{
	//reference inventory object as scriptable obj
	public InventoryObject inventory;
	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
	//reference get player level
	[NonSerialized]
	public Player playerLevel;

	[NonSerialized]
	public UIManager uIManager;


	void Start()
	{
		uIManager = FindObjectOfType<UIManager>();
		playerLevel = FindObjectOfType<Player>();
		if (playerLevel == null)
			Debug.Log("player not found");


		//loop throught the inventory slot of the getslot
		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			inventory.GetSlots[i].uiparent = this;
			inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
		}
		//create the slot on user interface
		CreateSlots();
		//add event to interface equip and slot pointer enter and pointer exit
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
		
	}
    private void Update()
    {
		//equipment obj not null
		if (equipmentObj != null)
		{
			//check window panel not null
			if(windowPop != null)
			{
				//check the pop window not active
				if (!windowPop.gameObject.activeInHierarchy)
				{
					//set control inventory = true
					infoEquip = true;
					//set control click to true
					controlClick = true;
				}
			}
		}
	}

    private void OnSlotUpdate(InventorySlot _slot)
	{
		
		//update slot item by checking the id number
		if (_slot.newItem.Id >= 0)
		{
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
			_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.newAmount == 1 ? "" : _slot.newAmount.ToString("n0");
		}
		else
		{
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
			_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
			_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}
	}
	public abstract void CreateSlots();
	
	//add event  as parameter object
	protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = obj.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}

	//reference color to store original color
	Color spriteOriginalColor;
	//reference eqipment obj
	private GameObject equipmentObj;
	//controlling pop out
	private bool infoEquip = true;
	//store current slot
	private GameObject storeSlot;
	//mouse enter to inventory slot slot to equal to obj
	public void OnEnter(GameObject slotPrefab)
	{
		//Debug.Log(MouseData.interfaceMouseIsOver);

		uIManager.controlSpawn = true;
		//later can change back
		spriteOriginalColor = slotPrefab.GetComponentInChildren<Image>().color;
		MouseData.slotHoveredOver = slotPrefab;
		storeSlot = slotPrefab;
		//controlling window pop out
		if (infoEquip)
		{
			if (slotsOnInterface[slotPrefab].ItemObject == null)
			{
				//Debug.Log("null");
				return;
			}
			else
			{
				//Debug.Log(slotsOnInterface[slotPrefab].ItemObject.newItem.Name);
				//check item  item is food
				if (slotsOnInterface[slotPrefab].ItemObject.itemType == ItemType.Healing)
				{
					consume = slotPrefab;
					//enable the button
					slotPrefab.GetComponent<Button>().enabled = true;
					//add trigger button to click the object
					//get the item function create consumables
					slotPrefab.GetComponent<Button>().onClick.AddListener(CreateConsumable);
				}
				else if (slotsOnInterface[slotPrefab].newItem.itemType == ItemType.Mana)
				{
					//mana consume
				}
				//item is not healing, mana and not the interface
				if (slotsOnInterface[slotPrefab].newItem.Id != -1 && slotsOnInterface[slotPrefab].newItem.itemType != ItemType.Healing &&
					slotsOnInterface[slotPrefab].newItem.itemType != ItemType.Mana)
				{
					//check for your the item level compare with the player current level
					if (slotsOnInterface[slotPrefab].newItem.itemLevel > playerLevel.LevelSystem.currentLevel)
					{
						//get the slot prefab image sprite
						//change the color
						slotPrefab.GetComponentInChildren<Image>().color = Color.red;

					}
					if (slotsOnInterface[slotPrefab].newItem.itemLevel == playerLevel.LevelSystem.currentLevel)
					{
						slotPrefab.GetComponentInChildren<Image>().color = spriteOriginalColor;
					}
					if (slotsOnInterface[slotPrefab].newItem.itemLevel < playerLevel.LevelSystem.currentLevel)
					{
						slotPrefab.GetComponentInChildren<Image>().color = Color.gray;
					}

					//add click on listener add function pop up window
					equipmentObj = slotPrefab;
					slotPrefab.GetComponent<Button>().enabled = true;
					slotPrefab.GetComponent<Button>().onClick.AddListener(PopWindow);
				}
			}


		}
	}

	//reference pop panel game object
	public GameObject PopPanel;
	//reference windowPop gameobject
	private GameObject windowPop;
	//reference control click
	private bool controlClick = true;
	//reference for the text attributes, name, item level and type
	private TextMeshProUGUI t_itemAttribute, t_itemName, t_itemLevel, t_itemType, t_itemRarity, t_itemValue;
	//function pop window
	public void PopWindow()
	{
		//check control click
		if (controlClick)
		{
			//set control click to false
			controlClick = false;
			//set control window to false
			infoEquip = false;
			//spawn pop up window at the obj item position
			var window = Instantiate(PopPanel, new Vector2(equipmentObj.transform.position.x, equipmentObj.transform.position.y), Quaternion.identity);
			window.transform.SetParent(equipmentObj.transform.parent);
			window.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
			//set the text item attribute, item name, item level and item type

			t_itemAttribute = GameObject.Find("AttributeText").GetComponent<TextMeshProUGUI>();
			t_itemLevel = GameObject.Find("ItemLevel").GetComponent<TextMeshProUGUI>();
			t_itemName = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
			t_itemType = GameObject.Find("TypeText").GetComponent<TextMeshProUGUI>();
			t_itemRarity = GameObject.Find("RarityText").GetComponent<TextMeshProUGUI>();
			t_itemValue = GameObject.Find("ValueText").GetComponent<TextMeshProUGUI>();
			windowPop = window;
			//check that item not null to set the text
			if (t_itemAttribute || t_itemLevel || t_itemName || t_itemType || t_itemRarity || t_itemValue != null)
			{
				//show information
				//item inventory class, attribute, name, item level
				//set the text of the inventory
				t_itemName.text ="Name: " + slotsOnInterface[equipmentObj].newItem.Name.ToString();
				t_itemLevel.text = "" + slotsOnInterface[equipmentObj].newItem.itemLevel.ToString();
				t_itemType.text = "Type: " + slotsOnInterface[equipmentObj].newItem.itemType.ToString();
				t_itemAttribute.text = "Attribute: " + slotsOnInterface[equipmentObj].newItem.attributes.ToString();
				t_itemRarity.text = "Rarity: " + slotsOnInterface[equipmentObj].ItemObject.rarity.ToString();
				t_itemValue.text = "Value: " + slotsOnInterface[equipmentObj].ItemObject.newItem.buffs[0].value.ToString();
				
			}
		}
	}
	//find player health
	private Player playerCurrentHealth = null;
	private bool consumeFood = true;
	//reference for the consume obj
	private GameObject consume;
	public void CreateConsumable()
	{
		
		if (playerCurrentHealth == null)
			playerCurrentHealth = FindObjectOfType<Player>();

		//decrease the quantity of consumable
		if (slotsOnInterface[consume].newAmount >= 0)
		{
			//decrease the food
			if(consumeFood)
            {
				consumeFood = false;
				slotsOnInterface[consume].newAmount -= 1;
				//disable the consumable object
				//add hitpoints
				//check max hp less than health
				if (playerCurrentHealth.playerCurrentHP <= playerCurrentHealth.playerMaxHP)
				{
					//can consume
					playerCurrentHealth.playerCurrentHP += slotsOnInterface[consume].newItem.consumables;
				}
				else
				{
					//not less to set that to max hp
					playerCurrentHealth.playerCurrentHP = playerCurrentHealth.playerMaxHP;
				}
				//call wait time consume
				StartCoroutine(EatingWaitForSeconds(2f));
				//update the the slot
				OnSlotUpdate(slotsOnInterface[consume]);
				//set consume to null


			}

		}
	}
	//waiting time when player consume
	public IEnumerator EatingWaitForSeconds(float time)
	{
		//disable the button cannot click
		consume.GetComponent<Button>().interactable = false;
		yield return new WaitForSeconds(time);
		consumeFood = true;
		//enable the button can click
		consume.GetComponent<Button>().interactable = true;
		//remove the consume from inventory after no more consume
		if (slotsOnInterface[consume].newAmount == 0)
		{
			slotsOnInterface[consume].RemoveItem();
		}
		//consume = null;
	}
	//mouse exit to  slot hovered is null
	public void OnExit(GameObject obj)
	{
		MouseData.slotHoveredOver = null;
		//Debug.Log(spriteOriginalColor);
		obj.GetComponentInChildren<Image>().color = spriteOriginalColor;
		storeSlot.GetComponent<Button>().enabled = false;
	}
	//mouse exit interface obj user interface mouse over is null
	public void OnExitInterface(GameObject obj)
	{
		MouseData.interfaceMouseIsOver = null;
	}
	//mouse enter interface get the object user interface
	public void OnEnterInterface(GameObject obj)
	{
		MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
	}
	//on the mouse drag type of the item object to create new item as an obj
	public void OnDragStart(GameObject obj)
	{
		//slot is has tag slot
		//slot has item object that not null
		//cantiinue
		//else return
		//Debug.Log(obj.gameObject.tag);
		if (obj.gameObject.tag == "Slot")
		{

			//Debug.Log(slotsOnInterface[obj].ItemObject);
			if (slotsOnInterface[obj].ItemObject != null)
			{
				MouseData.tempItemBeingDragged = CreateTempItem(obj);
			}
			else
			{
				return;
			}
		}

	}
	//create temp item  mouse is drag
	public GameObject CreateTempItem(GameObject obj)
	{
		//refernce tmp item to null
		GameObject tempItem = null;
		//check item slot dictionary by accessing obj item identity
		if(slotsOnInterface[obj].newItem.Id >= 0)
		{
			//temp item as new obj
			tempItem = new GameObject();
			//add component rect transform component to the temp obj
			var rt = tempItem.AddComponent<RectTransform>();
			//change size of the rect
			rt.sizeDelta = new Vector2(100, 100);
			//transform to the parent
			tempItem.transform.SetParent(transform.parent);
			//add image component of the temp item
			var img = tempItem.AddComponent<Image>();
			img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
			img.raycastTarget = false;
		}

		return tempItem;
	}

	private string gameObjName;
	private InventorySlot objToSell;
	private Vector2 lastMousePos;
	//end Drag
	public void OnDragEnd(GameObject obj)
	{
		//check obj is null return
		//check is there item in the slot inventory
		if (obj.gameObject.tag == "Slot")
		{
			if (slotsOnInterface[obj].ItemObject != null)
			{

				gameObjName = slotsOnInterface[obj].ItemObject.newItem.Name;
				objToSell = slotsOnInterface[obj];
				//Debug.Log(gameObjName);
				//Debug.Log(MouseData.interfaceMouseIsOver);
				Destroy(MouseData.tempItemBeingDragged);
				//want to drag outside the inventory or equipment screen
				if (MouseData.interfaceMouseIsOver == null)
				{
					lastMousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

					uIManager.DropUI.SetActive(true);
					uIManager.dropItemName.text = "" + slotsOnInterface[obj].newItem.Name;
					uIManager.dropItemText.text = "Are you sure you want to drop this?";

					GameObject.Find("YesButton").GetComponent<Button>().onClick.RemoveAllListeners();
					//parse the parameter of string of the slot name to create new object
					GameObject.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => uIManager.ConfirmYesDrop(gameObjName, lastMousePos, slotsOnInterface[obj]));
					//gameObjName = null;
					//remove the slot obj of the slot interface
					//slotsOnInterface[obj].RemoveItem();
					return;
				}

				//check for mouse data slot is hoverred on the object
				//Debug.Log(MouseData.slotHoveredOver.name);
				if (MouseData.slotHoveredOver)
				{
					//Debug.Log(MouseData.interfaceMouseIsOver);
					if (MouseData.interfaceMouseIsOver)
					{
						//ebug.Log("on drag end slot on interface item object is" + slotsOnInterface[obj].newItem.Name);
						//Debug.Log(slotsOnInterface[obj].newItem.itemLevel);
						//check for is inventory screen other is equipment screen
						if (MouseData.interfaceMouseIsOver.name == "InventoryScreen")
						{
							//check for the object name
							//enable swap items
							//refer to inventory slot as mouse hover slot data to get interface mouse is over of the slots interface
							//Debug.Log(MouseData.slotHoveredOver);
							InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
							//Debug.Log(mouseHoverSlotData);
							//Debug.Log(slotsOnInterface[obj].ItemObject.name);
							//call inventory to swap items on slot interface array of obj to the inventory slot mouse hover slot data
							inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
							//OverwriteSlots();
						}
						if (MouseData.interfaceMouseIsOver.name == "EquipmentScreen")
						{
							//Debug.Log(slotsOnInterface[obj].newItem.itemLevel);
							if (playerLevel.LevelSystem.currentLevel >= slotsOnInterface[obj].newItem.itemLevel)
							{

								//Debug.Log("equip");
								//check for the object name
								//enable swap items
								//refer to inventory slot as mouse hover slot data to get interface mouse is over of the slots interface
								InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
								//call inventory to swap items on slot interface array of obj to the inventory slot mouse hover slot data
								inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
							}
							else
							{
								Debug.Log("you level is not enough to wear this item");
							}
						}
						if (MouseData.interfaceMouseIsOver.name == "DropScreen")
						{
							//InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
							InventorySlot selectedObjSell = FindObjectOfType<ShopSytem>().GetSelectedObjToSell(objToSell);

						}
					}
				}
				//slotsOnInterface[obj] = null;
			}
			else
			{
				return;
			}
		}

	}
	//item on the mouse
	//this parameter obj not being use
	public void OnDrag(GameObject obj)
	{
		//temp item being dragged on the mouse get component rect transform position to equal mouse position
		if (MouseData.tempItemBeingDragged != null)
			MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;

	}
}

public static class MouseData
{
	public static UserInterface interfaceMouseIsOver;
	public static GameObject tempItemBeingDragged;
	//reference object type of slot empty
	public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
	public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
	{
		foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
		{
			if (_slot.Value.newItem.Id >= 0)
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.newAmount == 1 ? "" : _slot.Value.newAmount.ToString("n0");
			}
			else
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
			}
		}
	}
}

