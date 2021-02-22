using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

public enum InterfaceType
{
	Inventory,
	Equipment,
	Chest,
	Bin
}

[CreateAssetMenu(fileName ="New Inventory", menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
	public string savePath;
	//reference data base
	public ItemDatabaseObject itemDataBase;
	public InterfaceType type; 
	//reference containter as inventory
	public Inventory Container;
	private Item thisItemObject;

	//get the  slots of an array inventory of 24
	public InventorySlot[] GetSlots { get { return Container.Slots; } }

	public bool AddItem(Item newItem, int _amount)
	{
		//have item on slot
		if (EmptySlotCount <= 0)
			return false;

		//check item at the slot for the same ID to get that slot item
		InventorySlot sameSlotID = FindItemOnInventory(newItem);
		//item database for the new item is it not healing or sameslot id is none
		if (!itemDataBase.ItemObjects[newItem.Id].stackable || sameSlotID == null)
		{
			//add slot to the item
			SetEmptySlot(newItem, _amount);
			return true;
		}
		//item not armor or equip add amount
		sameSlotID.AddHealing(_amount);
		//set item level of this to pick up item level
		thisItemObject.itemLevel = newItem.itemLevel;
		return true;
	}

	public int EmptySlotCount
	{

		get
		{
			//loop through slot length
			int counter = 0;
			for (int slots = 0; slots < GetSlots.Length; slots++)
			{
				//check for slot is empty
				//increment the counter
				if (GetSlots[slots].newItem.Id <= -1)
					counter++;
			}
			return counter;
		}
	}

	public InventorySlot FindItemOnInventory(Item _item)
	{
		//loop througth the slot length
		for (int slots = 0; slots < GetSlots.Length; slots++)
		{
			//check the slot for an item i is same with the item id that pick.
			if(GetSlots[slots].newItem.Id == _item.Id)
			{
				return GetSlots[slots];
			}
		}
		return null;
	}

	public InventorySlot SetEmptySlot(Item _item, int _amount)
	{
		//loop throught the get slots length
		for (int slots = 0; slots < GetSlots.Length; slots++)
		{
			//check for the get slots item id equal to -1
			if(GetSlots[slots].newItem.Id <= -1)
			{
				//get the current slots
				GetSlots[slots].UpdateSlot(_item, _amount);
				return GetSlots[slots];
			}
		}
		//check the item level compare to player level
		//set up for what happens when inventory is full (later addon)
		for (int slots = 0; slots < GetSlots.Length; slots++)
		{
			//check for the item slots 1 to item slot 24
			Debug.Log(GetSlots[slots].ItemObject.name);
		}
		return null;
	}
	//this called on user interface class
	public void SwapItems(InventorySlot invnSloItem1, InventorySlot invnSloItem2)
	{
		//check inventory slot 1 can place in slot invn slot 2
		if (invnSloItem1.CanPlaceInSlot(invnSloItem2.ItemObject) && invnSloItem2.CanPlaceInSlot(invnSloItem1.ItemObject))
		{
			//store inventory slot on mouse hover as new inventory slot item and the amount
			InventorySlot t_invnSlotItem2 = new InventorySlot(invnSloItem2.newItem, invnSloItem2.newAmount);
			InventorySlot t_invnSlotItem1 = new InventorySlot(invnSloItem1.newItem, invnSloItem1.newAmount);
			//Debug.Log(t_invnSlotItem1);
			//Debug.Log(t_invnSlotItem2.ItemObject);
			//update mouse over slot as inv slot item and amount
			invnSloItem2.UpdateSlot(t_invnSlotItem1.newItem, invnSloItem1.newAmount);
			////update invt slot to temperature mouse over slot and amount
			invnSloItem1.UpdateSlot(t_invnSlotItem2.newItem, t_invnSlotItem2.newAmount);
			

		}
		
	}

	public void RemoveItem(Item _item)
	{
		for (int i = 0; i < GetSlots.Length; i++)
		{
			if(GetSlots[i].newItem == _item)
			{
				GetSlots[i].UpdateSlot(null, 0);
			}
		}
	}

	[ContextMenu("Save")]
	public void Save()
	{
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
		formatter.Serialize(stream, Container);
		stream.Close();
	}	
	[ContextMenu("Load")]
	public void Load()
	{
		if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
			Inventory newContainer = (Inventory)formatter.Deserialize(stream);
			for (int i = 0; i < GetSlots.Length; i++)
			{
				GetSlots[i].UpdateSlot(newContainer.Slots[i].newItem, newContainer.Slots[i].newAmount);
			}
			stream.Close();
		}
	}	
	[ContextMenu("Clear")]
	public void Clear()
	{
		Container.Clear();
	}
}
[System.Serializable]
public class Inventory
{
	//set slot to equal new slot of 24
	public InventorySlot[] Slots = new InventorySlot[24];
	
	public void Clear()
	{
		//loop through all the slot length of the current slot
		for (int currentSlot = 0; currentSlot < Slots.Length; currentSlot++)
		{
			//remove all the slot
			Slots[currentSlot].RemoveItem();
		}
	}
}

//use delegate as slotupdated
public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
	//initialize allow items of type is Healing
	public ItemType[] AllowType = new ItemType[0];
	[System.NonSerialized]
	public UserInterface uiparent;
	[System.NonSerialized]
	public GameObject slotDisplay;
	//delegate
	[System.NonSerialized]
	public SlotUpdated OnAfterUpdate;
	//delegate
	[System.NonSerialized]
	public SlotUpdated OnBeforeUpdate;
	//initialize item as new item
	public Item newItem = new Item();
	public int newAmount = 0;
	[System.NonSerialized]
	public InventoryObject inventoryObj;


	//calling class object item obj to get
	public ItemObject ItemObject
	{
		get
		{
			//red need to check this
			//check item id more than zero
			if(newItem.Id >= 0)
			{
				//get the user interface parent inventory of itemdabase item obj of that item number
				return uiparent.inventory.itemDataBase.ItemObjects[newItem.Id];
			}
			return null;
		}
	}

	public InventorySlot()
	{
		UpdateSlot(new Item(), 0);
	}
	public InventorySlot(Item _item, int _amount)
	{
		UpdateSlot(_item, _amount);
	}
	//update slot inventory  as parameter object item and amount
	public void UpdateSlot(Item _item, int _amount)
	{
		//delegate on before not null to invoke
		if (OnBeforeUpdate != null)
			OnBeforeUpdate.Invoke(this);
		//slot item with the amount update

		newItem = _item;
		newAmount = _amount;
		if (OnAfterUpdate != null)
			OnAfterUpdate.Invoke(this);

	}
	//remove item with amount of zero
	public void RemoveItem()
	{
		UpdateSlot(new Item(), 0);
	}

	//add amount to current item
	public void AddHealing (int value)
	{
		//update slot by increase the amount of the item
		UpdateSlot(newItem, newAmount += value);
	}
	//check can place slot in as parameter
	public bool CanPlaceInSlot(ItemObject invntrySlot)
	{
		
		//the item that is allow less than zero that is food or item object is none or new item object  is less than zero
		if (AllowType.Length <= 0 || invntrySlot == null || invntrySlot.newItem.Id < 0)
			return true;
		//loop through number of item type that is allow which is zero
		for (int itemType = 0; itemType < AllowType.Length; itemType++)
		{
			//check the food item type that is allow same
			//check the name is it same
			if (invntrySlot.itemType == AllowType[itemType])
				return true;
		}
		return false;
	}
}
