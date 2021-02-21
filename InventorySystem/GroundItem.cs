using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject itemObject;

    private void Start()
    {
		//calling coroutine
		StartCoroutine(SelfDestroy());
    }
    public void OnAfterDeserialize()
	{

	}
	//destroy the object after 3 min
	IEnumerator SelfDestroy()
	{
		yield return new WaitForSeconds(180f);
		Destroy(gameObject);
	}

	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		GetComponent<SpriteRenderer>().sprite = itemObject.uiDisplay;
		EditorUtility.SetDirty(GetComponent<SpriteRenderer>());
#endif
	}
}
