using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private int amount;
	public int minAmount;
	public int maxAmount;
    private Player thePlayer;

	private void Start()
	{
		thePlayer = FindObjectOfType<Player>();
		amount = Random.Range(minAmount, maxAmount);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			thePlayer.AddGold(amount);
			Destroy(gameObject);
		}
	}
}
