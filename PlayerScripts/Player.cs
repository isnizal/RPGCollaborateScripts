using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed;
    private Animator animator;
    private Vector3 movement;
    private Rigidbody2D myRigidbody;

    [Header("Player Stats")]
    public int playerMaxHP;
    public int playerCurrentHP;
    //add player max mana and current mana
    public int playerMaxMana;
    public int playerCurrentMana;
    public int playerAttackPower;
    public int playerDefensePower;
    public int playerIntelligencePower;
    [Space]
    public int statPoints;
    public int statPointsAllocated;
    [Space]
    public Level LevelSystem;
    [Space]
    public Attribute[] attributes;
    public ModifiableInt statModifier;//

    [Header("Damage Effects")]
    public GameObject damageNumber;

    [Header("Player Inventory/Equipment")]
    public InventoryObject inventory;
    public InventoryObject equipment;

    [Header("Currency Manager")]
    public TextMeshProUGUI currentValueText;
    public int currentGold;

    private EnemyStats theEnemy;
    private UIManager ui;
    
  
    void Start()
    {
        ui = GetComponent<UIManager>();
        //set attribute and equipment
		for (int i = 0; i < attributes.Length; i++)
		{
            attributes[i].SetParent(this);

		}

		for (int i = 0; i < equipment.GetSlots.Length; i++)
		{
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

        }
        SetMaxPlayerHealth();
        SetMaxPlayerMana();
        LevelSystem = new Level(1, OnLevelUp);

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    public void OnLevelUp()
	{
       
        statPoints = Random.Range(1, 5);
        IncreaseMaxHpForClass();
        print("Level Up!");
	}
    //function class increase max hitpoints
    void IncreaseMaxHpForClass()
    {
        if (name == "Warrior")
        {
            float newPlayerMaxHp = playerMaxHP * 1.3f;
            playerMaxHP = Mathf.RoundToInt(newPlayerMaxHp);
        }
        else if (name == "Archer")
        {
            float newPlayerMaxMp = playerMaxMana *0.8f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxMp);
        }
        else if (name == "Mage")
        {
            float newPlayerMaxMp = playerMaxMana * 0.5f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxMp);
        }
    }
    public void OnBeforeSlotUpdate(InventorySlot _slot)
	{
        //check slot is null of the item object is null to return
        if (_slot.ItemObject == null)
            return;
        //checked for slot type
		switch (_slot.uiparent.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
                //remove slot of the item object attached, on the inventory type of the slot, allow the item 
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.uiparent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowType)));

                //loop through the slot new item buff modifier
                for (int i = 0; i < _slot.newItem.buffs.Length; i++)
                {
                    //loop through
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        //check for the attribute through loop type equal to slot new item
                        //remove modifier value of the slot new item buff
                        if (attributes[j].attributeType == _slot.newItem.buffs[i].attribute)
                            attributes[j].modifiableValue.RemoveModifier(_slot.newItem.buffs[i]);
                    }
                }
                break;
			case InterfaceType.Chest:
				break;
			default:
				break;
		}
	}
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.uiparent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.uiparent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowType)));

				for (int i = 0; i < _slot.newItem.buffs.Length; i++)
				{
					for (int j = 0; j < attributes.Length; j++)
					{
                        if (attributes[j].attributeType == _slot.newItem.buffs[i].attribute)
                            //add modifier value slot buffs
                            attributes[j].modifiableValue.AddModifier(_slot.newItem.buffs[i]);
					}
				}

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    private bool arrowSpawn;
    private bool canFire = true;
    private bool orbSpawn;
    void Update()
    {
        currentValueText.text = "" + currentGold;
        if (Input.GetKeyDown(KeyCode.S))
		{
            inventory.Save();
            equipment.Save();
		}
        if(Input.GetKeyDown(KeyCode.L))
		{
            inventory.Load();
            equipment.Load();
		}            
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        AnimationUpdate();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (this.name == "Archer")
            {
                if (arrowSpawn)
                {
                    if (canFire)
                    {
                        canFire = false;
                        StartCoroutine(Attacking("Archer"));
                    }
                }
            }
            else if (this.name == "Mage")
            {
                if (orbSpawn)
                {
                    if (canFire)
                    {
                        canFire = false;
                        StartCoroutine(Attacking("Mage"));
                    }
                }
            }
            else
            {
                StartCoroutine(Attacking("Warrior"));
            }
        }

        if (playerCurrentHP <= 0)
		{
            gameObject.SetActive(false);
		}

    }
	void AnimationUpdate()
    {
        if (movement != Vector3.zero)
        {
            if (this.name == "Archer" || this.name == "Mage")
            {
                if (canFire)
                {
                    MovePlayer();
                    animator.SetFloat("moveX", movement.x);
                    animator.SetFloat("moveY", movement.y);
                    animator.SetBool("moving", true);
                    arrowSpawn = false;
                    orbSpawn = false;
                }
            }
            else
            {
                MovePlayer();
                animator.SetFloat("moveX", movement.x);
                animator.SetFloat("moveY", movement.y);
                animator.SetBool("moving", true);
                arrowSpawn = false;
                orbSpawn = false;
            }
        }
        else
        {
            arrowSpawn = true;
            orbSpawn = true;
            animator.SetBool("moving", false);
            myRigidbody.velocity = Vector2.zero;
        }
    }
    public GameObject arrowPrefab;
    public GameObject orbPrefab;
    public float archerRateOfFire = 0.6f;
    public float mageRateOfFire = 1f;
    private IEnumerator Attacking(string warriorClass)
	{
        if (warriorClass == "Warrior")
        {
            animator.SetBool("attacking", true);
            yield return null;
            animator.SetBool("attacking", false);
            yield return new WaitForSeconds(.3f);
        }
        else if (warriorClass == "Archer")
        {
            animator.SetBool("attacking", true);
            yield return new WaitForSeconds(archerRateOfFire);
            animator.SetBool("attacking", false);
            canFire = true;
        }
        else if (warriorClass == "Mage")
        {
            animator.SetBool("attacking", true);
            yield return null;
            animator.SetBool("attacking", false);
            yield return new WaitForSeconds(mageRateOfFire);
            canFire = true;
        }
	}
    public void SpawnArrowUp()
    {

        if (this.name == "Archer")
        {
                GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, 45)));
                arrow.transform.SetParent(transform);
                arrow.GetComponent<ArrowMove>().enableUpMove = true;

        }
        else if (this.name == "Mage")
        {

            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableUpMove = true;
        }

    }
    public void SpawnArrowLeft() {
        if (this.name == "Archer")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, 135)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableLeftMove = true;
        }
        else if (this.name == "Mage")
        {
            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableLeftMove = true;
        }
    }
    public void SpawnArrowRight() {
        if (this.name == "Archer")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, -45)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableRightMove = true;
        }
        else if (this.name == "Mage")
        {
            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableRightMove = true;
        }
    }
    public void SpawnArrowDown() {
        if (this.name == "Archer")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, -135)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableDownMove = true;
        }
        else if (this.name == "Mage")
        {
            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableDownMove = true;
        }
    }
    void MovePlayer()
	{
      movement.Normalize();
      myRigidbody.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
       
    }

	public void DamageToPlayer(int enemyDamage,GameObject enemy)
    {
        //store player last hitpoints
        int playerLastHitpoints = playerCurrentHP;
        //check player has a defense if have absorb the damage
        if (playerDefensePower != 0)
        {
            //initialize defense absorb
            int armorAbsorb = playerDefensePower / 2;
            //check enemy damage exceed player health and armor
            if (enemyDamage >=  playerCurrentHP && enemyDamage >= armorAbsorb)
            {
                //call the ui
                enemy.GetComponent<EnemyStats>().UpdateUIAttack(enemyDamage);
                //player suppose to be dead
                playerCurrentHP = 0;
            }
            //player defense is higher than the enemy attack deal no damage
            if (armorAbsorb >= enemyDamage)
            {
                //call the ui
                enemy.GetComponent<EnemyStats>().UpdateUIAttackForMiss("MISS");
                // player health same
                playerCurrentHP = playerLastHitpoints;
            }
            //check for the damage more than defense absorb
            if (enemyDamage >= armorAbsorb)
            {
                //get the enemy damage after minus with the armor absorb
                enemyDamage -= armorAbsorb;
                //calling the ui damage of the enemy
                enemy.GetComponent<EnemyStats>().UpdateUIAttack(enemyDamage);
                //set the hitpoints to minus to current enemy damage
                playerCurrentHP -= enemyDamage;

            }
            else
            {
                Debug.Log("error, damage cannot be done");
            }
        }
        else
        {
            // no armor
            enemy.GetComponent<EnemyStats>().UpdateUIAttack(enemyDamage);
            playerCurrentHP -= enemyDamage;
        }

    }
	public void SetMaxPlayerHealth()
	{
        playerCurrentHP = playerMaxHP;
	}
    public void SetMaxPlayerMana()
    {
        playerCurrentMana = playerMaxMana;
    }
    public void AddGold(int amount)
	{
        currentGold += amount;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
            other.gameObject.GetComponent<EnemyStats>().DamageToEnemy(playerAttackPower);
            var clone = (GameObject) Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = playerAttackPower;
		}
        //check for ground item
        var groundItem = other.GetComponent<GroundItem>();
        if(groundItem)
		{
            //initialize new item function item object
            Item newItem = new Item(groundItem.itemObject);
            if(inventory.AddItem(newItem , 1))
			{
                StartCoroutine(WaitForSeconds(0.3f, other.gameObject));
			}        
		}
	}
    //wait before destroy
    IEnumerator WaitForSeconds(float time, GameObject item)
    {
        //set the time to specified
        yield return new WaitForSeconds(time);
        //call the destroy object
        Destroy(item.gameObject);
    }
    public void AttributeModified(Attribute attribute)
	{
        Debug.Log(string.Concat(attribute.attributeType, " was updated! Value is now ", attribute.modifiableValue.ModifiedValue));
        var tempattackvalue = attributes[0].modifiableValue.ModifiedValue;
        var tempdefensevalue = attributes[1].modifiableValue.ModifiedValue;
        var tempintelligencevalue = attributes[2].modifiableValue.ModifiedValue;
        playerAttackPower = tempattackvalue;
        playerDefensePower = tempdefensevalue;
        playerIntelligencePower = tempintelligencevalue;
	}
	//Inventory clean on quit. (temp method)
	private void OnApplicationQuit()
	{
        inventory.Clear();
        equipment.Clear();
	}
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes attributeType;
    public ModifiableInt modifiableValue;

    public void SetParent(Player _parent)
	{
        parent = _parent;
        modifiableValue = new ModifiableInt(AttributeModified);
	}
    public void AttributeModified()
	{
        parent.AttributeModified(this);
	}
}
