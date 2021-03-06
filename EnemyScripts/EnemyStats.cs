﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	[Header("Enemy Stats")]
	public int enemyID;
	public string enemyName;
    public int enemyCurrentHP;
	public int enemyMaxHP;
    public int enemyDefense;
    public int enemyAttackPower;
	public int expToGive;
	
	private Player thePlayer;
	private Spawner spawner;

	[Header("Damage Effects")]
	public GameObject damageNumber;

	[Header("Loot Table")]
	public int[] table;
	[Space]
	public List<GameObject> Items;
	[Space]
	public GameObject goldPrefab;
	private float randomItemSpawn;
	private int total;
	[HideInInspector]
	public Vector2 lastEnemyPos;
	private int randomItemDrop;
	private Rigidbody2D myrb;

	private void Start()
	{
		SetMaxEnemyHealth();
		thePlayer = FindObjectOfType<Player>();
		spawner = FindObjectOfType<Spawner>();
		myrb = GetComponent<Rigidbody2D>();
	}
	private bool b_enemyDead = false;
	private void Update()
	{
		randomItemDrop = Random.Range(0, 3);
		if (enemyCurrentHP <= 0)
		{
			//store enemy position before die
			lastEnemyPos = this.transform.position;
			Destroy(this.gameObject);
			b_enemyDead = true;
			thePlayer.LevelSystem.AddExp(expToGive);
		}	
	}
	public void DamageToEnemy(int damageToGive)
	{
		enemyCurrentHP -= damageToGive;
	}
	public void SetMaxEnemyHealth()
	{
		enemyCurrentHP = enemyMaxHP;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<Player>().DamageToPlayer(enemyAttackPower,this.gameObject);
		}
	}
	//making new function Update UI enemy attack
	public void UpdateUIAttack(int enemyAttackPower)
	{
		//set the floating number to this enemy attack power
		var clone = Instantiate(damageNumber, thePlayer.transform.position, Quaternion.Euler(Vector3.zero));
		clone.GetComponent<FloatingNumbers>().changeUI = true;
		clone.GetComponent<FloatingNumbers>().damageNumber = enemyAttackPower;
	}
	//miss damage
	public void UpdateUIAttackForMiss(string enemyAttackPower)
	{
		//set the floating number to this enemy attack power
		//spawn obj at player transform
		var clone = Instantiate(damageNumber, thePlayer.transform.position, Quaternion.Euler(Vector3.zero));
		clone.GetComponent<FloatingNumbers>().changeUI = false;
		//get the object damage number floating point
		clone.GetComponent<FloatingNumbers>().damageCharac = enemyAttackPower;

	}
	//calling on destroy function after enemy destroy
	private void OnDestroy()
	{
		//call the spawn  item function
		if (b_enemyDead)
		{
			SpawnItem();
			if(enemyID == 1)
				spawner.slimeCounter -= 1;

			if(enemyID == 2)
				spawner.goblinCounter -= 1;

			if (enemyID == 3)
				spawner.slime2Counter -= 1;

			if (enemyID == 4)
				spawner.mushroomCounter -= 1;

			if (enemyID == 5)
				spawner.skeletonCounter -= 1;

			if (enemyID == 6)
				spawner.skullCounter -= 1;

			if (enemyID == 7)
				spawner.rockCounter -= 1;

			if (enemyID == 8)
				spawner.arachnaCounter -= 1;
		}
	}

	//make function SpawnItem
	void SpawnItem()
	{
		//get random item spawn from the random range
		randomItemSpawn = Random.Range(0, 1f);
		//Debug.Log(randomItemSpawn);
		//10 percent spawn random item
		if (randomItemSpawn <= 0.1f || randomItemSpawn <= 0.05f)
		{
			//spawn random item from the random table
			int getRandomItem = Random.Range(0, Items.Count);
			if (b_enemyDead)
			{
				b_enemyDead = false;
				GameObject item = Instantiate(Items[getRandomItem], new Vector2(lastEnemyPos.x, lastEnemyPos.y), Quaternion.identity) as GameObject;
			}

		}
		//90 percent spawn random gold
		else if (randomItemSpawn > 0.1f && randomItemSpawn <= 1f)
		{
			if (b_enemyDead)
			{
				b_enemyDead = false;
				//spawn gold with different random gold value
				GameObject gold = Instantiate(goldPrefab, new Vector2(lastEnemyPos.x, lastEnemyPos.y), Quaternion.identity);
			}
		}
		//loop through the number of the table
		//for (int i = 0; i < table.Length; i++)
		//{
		//	//check random number less than current table
		//	if (randomItemSpawn <= table[i])
		//	{
		//		GameObject item = Instantiate(Items[randomItemDrop], new Vector2(lastEnemyPos.x, lastEnemyPos.y), Quaternion.identity) as GameObject;
		//		//item.transform.SetParent(GameObject.Find("ParentItemDrop").transform);
		//		return;
		//	}
		//	else { randomItemSpawn -= table[i]; }
		//}
	}
}
