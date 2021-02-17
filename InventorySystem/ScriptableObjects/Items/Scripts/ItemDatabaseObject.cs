using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemObject[] ItemObjects;
	public void UpdateID()
	{
		//loop through itemobject length
		for (int itemNumb = 0; itemNumb < ItemObjects.Length; itemNumb++)
		{
			//check for that item object of the new data id not equal to this item object number
			//assign current object number  to this item number
			if (ItemObjects[itemNumb].newItem.Id != itemNumb)
				ItemObjects[itemNumb].newItem.Id = itemNumb;
		}
	}

	public void OnAfterDeserialize()
	{
		UpdateID();
	}

	public void OnBeforeSerialize()
	{
		
	}
}
