using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
	//reference gameobject slot for the equipment objects to equip
    public GameObject[] slots;
    
	//create slot
	public override void CreateSlots()
	{
		//reference slot on interface as gameobject and inventory slot
		slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
		//loop through inventory equipment of slot that is 5
		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			//reference obj of game object slot to set the slot
			var obj = slots[i];

			AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
			AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
			AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
			AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
			AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

			//set the slot display to equal to obj
			inventory.GetSlots[i].slotDisplay = obj;
			//add game object slot  with type of inventory slot get slots to the dictionary
			slotsOnInterface.Add(obj, inventory.GetSlots[i]);
		}
	}
}
