using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Default,
	Healing,
	Helmet,
	Weapon,
	Bow,
	Arrow,
	Wand,
	Shield,
	Boots,
	Chest,
	Mana,
	
}

public enum Rarity
{
	None,
	Uncommon,
	Common,
	Rare,
	Epic,
	Legendary
}

public enum Attributes
{
	Strength,
	Defense,
	Intelligence,
	Dexterity,
	NonAttributes

}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
	public Sprite uiDisplay;
	//refer to the food item meat stackable
	public bool stackable;
	public ItemType itemType;
	public Rarity rarity;
	[TextArea(15,20)]
	public string description;
	//assign new data to the item
	public Item newItem = new Item();

	private void Awake()
	{
		
	}
	//create new item of the object to called
    public Item CreateItem()
	{
		Item newItem = new Item(this);
		return newItem;
	}
}


[System.Serializable]
//characteristic item
public class Item
{
	public string Name;
	public int Id = -1;
	public ItemBuff[] buffs;
	

	public int itemPrice;
	public int itemLevel;
	//create consumable item for hitpoints
	public int consumables;



	//reference item object and buff to display this information on screen
	[HideInInspector]
	public ItemType itemType;
	[HideInInspector]
	public Attributes attributes;


	public Rarity itemRarity { get; }
	[HideInInspector]
	public int values;
	private int saveValue;
	

	public Item()
	{
		Name = "";
		//Id = -1;
	}
	public Item(ItemObject itemObj)
	{
		//I change this name for a while to display the name on screen
		Name = itemObj.newItem.Name;
		Id = itemObj.newItem.Id;
		itemLevel = itemObj.newItem.itemLevel;
		consumables = itemObj.newItem.consumables;

		//new one i think
		itemType = itemObj.itemType;
		attributes = itemObj.newItem.buffs[0].attribute;
		itemRarity = itemObj.rarity;
		values = itemObj.newItem.buffs[0].GenerateValue();
		saveValue = values;
		buffs = new ItemBuff[itemObj.newItem.buffs.Length];

		//loop through the buffs length if have many
		for (int i = 0; i < buffs.Length; i++)
		{
			//get the new item buff from looping to equal new item buff min and max
			buffs[i] = new ItemBuff(itemObj.newItem.buffs[i].min, itemObj.newItem.buffs[i].max);
			{

				//the current buff attribute to equal to new data buff attribue
				buffs[i].attribute = itemObj.newItem.buffs[i].attribute;
			}

		}
	}
}

[System.Serializable]
public class ItemBuff : IModifiers
{
	public Attributes attribute;

	public  int min;
	public  int max;
	[HideInInspector]
	public int value;
	public ItemBuff(int _min, int _max)
	{
		min = _min;
		max = _max;
		GenerateValue();
	}

	public void AddValue(ref int baseValue)
	{
		baseValue += value;
	}
	
	public int GenerateValue()
	{
		return value = Random.Range(min, max);

	}
}