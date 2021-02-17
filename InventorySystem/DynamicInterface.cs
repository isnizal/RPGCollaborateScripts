using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
	public GameObject inventoryPrefab;

	[Header("Display Position Settings")]
	public int X_START;
	public int Y_START;
	public int X_SPACE_BETWEEN_ITEM;
	public int Y_SPACE_BETWEEN_ITEM;
	public int NUMBER_OF_COLUMN;

	public override void CreateSlots()
	{
		//refer slot to save progress
		slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
		//inventory inherit from user interface get the slot number
		for (int numberSlot = 0; numberSlot < inventory.GetSlots.Length; numberSlot++)
		{
			//instantiate inventory slot object  at zero position transform to this position
			var slotPrefab = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity,transform);
			slotPrefab.name = "slot " + numberSlot;
			//set the position of the local
			slotPrefab.GetComponent<RectTransform>().localPosition = GetPosition(numberSlot);
			//add event to the function as event trigger type
			AddEvent(slotPrefab, EventTriggerType.PointerEnter, delegate { OnEnter(slotPrefab); });
			AddEvent(slotPrefab, EventTriggerType.PointerExit, delegate { OnExit(slotPrefab); });
			AddEvent(slotPrefab, EventTriggerType.BeginDrag, delegate { OnDragStart(slotPrefab); });
			AddEvent(slotPrefab, EventTriggerType.EndDrag, delegate { OnDragEnd(slotPrefab); });
			AddEvent(slotPrefab, EventTriggerType.Drag, delegate { OnDrag(slotPrefab); });
			//get the inventory object of the get slots container
			inventory.GetSlots[numberSlot].slotDisplay = slotPrefab;
			slotsOnInterface.Add(slotPrefab, inventory.GetSlots[numberSlot]);
			
		}
	}
	private Vector3 GetPosition(int numberSlot)
	{
		//spawn left to right,downwards
		return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (numberSlot % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (numberSlot / NUMBER_OF_COLUMN)), 0f);
	}

}
