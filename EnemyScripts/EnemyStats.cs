using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	[Header("Enemy Stats")]
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
	private int randomNumber;
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
			Destroy(gameObject);
			b_enemyDead = true;
			spawner.spawnCounter--;
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
			GameObject gold = Instantiate(goldPrefab, new Vector2(lastEnemyPos.x, lastEnemyPos.y), Quaternion.identity);
			b_enemyDead = false;
		}
	}

	//make function SpawnItem
	void SpawnItem()
	{
		foreach (var item in table)
		{
			total += item;
		}
		randomNumber = Random.Range(0, total);

		for (int i = 0; i < table.Length; i++)
		{
			if (randomNumber <= table[i])
			{
				GameObject item = Instantiate(Items[randomItemDrop], new Vector2(lastEnemyPos.x, lastEnemyPos.y), Quaternion.identity) as GameObject;
				//item.transform.SetParent(GameObject.Find("ParentItemDrop").transform);
				return;
			}
			else { randomNumber -= table[i]; }
		}
	}
}
