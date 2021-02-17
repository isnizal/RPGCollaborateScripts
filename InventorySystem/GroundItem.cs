using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject itemObject;

	//on start check for the item sword or armor
	//set sword and armor level can equip
	public void OnAfterDeserialize()
	{

	}

	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		GetComponent<SpriteRenderer>().sprite = itemObject.uiDisplay;
		EditorUtility.SetDirty(GetComponent<SpriteRenderer>());
#endif
	}
}
