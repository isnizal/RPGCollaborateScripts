using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public float playerDexterityPower;
    [Space]
    public int statPoints;
    public float statPointsAllocated;
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

    private string currentCharacterSelection;
    [SerializeField]
    //save game
    private TextMeshProUGUI gameSaveText;

    public static Player instance;
  
    void Start()
    {
        
        if (instance != null)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            instance = this;
        }

		//ui = GetComponent<UIManager>();
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
        if (FindObjectOfType<CharacterSelection>())
        {
            currentCharacterSelection = FindObjectOfType<CharacterSelection>().saveCharacterName;
        }
        else
        {
            Debug.Log("cannot find character selection");
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
        if (name == "Warrior(Clone)")
        {
            float newPlayerMaxHp = playerMaxHP * 1.3f;
            playerMaxHP = Mathf.RoundToInt(newPlayerMaxHp);
            float newPlayerMaxMp = playerMaxMana * 0.5f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxMp);

        }
        else if (name == "Archer(Clone)")
        {
            float newPlayerMaxHp = playerMaxHP *0.8f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxHp);
            float newPlayerMaxMp = playerMaxMana * 0.5f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxMp);
        }
        else if (name == "Mage(Clone)")
        {
            float newPlayerMaxMp = playerMaxMana * 1.3f;
            playerMaxMana = Mathf.RoundToInt(newPlayerMaxMp);
            float newPlayerMaxHp = playerMaxHP * 0.5f;
            playerMaxHP = Mathf.RoundToInt(newPlayerMaxHp);
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
    public int playerExperience;
    public int playerLevel;
    public float playerPositionX;
    public float playerPositionY;
    private bool gameSave = true;
    
    void Update()
    {
        currentValueText.text = "" + currentGold;
         
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        SaveAndLoadGame();
        AnimationUpdate();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (this.name == "Archer(Clone)")
            {
                if (arrowSpawn)
                {
                    if (canFire)
                    {
                        canFire = false;
                        StartCoroutine(Attacking("Archer(Clone)"));
                    }
                }
            }
            else if (this.name == "Mage(Clone)")
            {
                if (orbSpawn)
                {
                    if (canFire)
                    {
                        canFire = false;
                        StartCoroutine(Attacking("Mage(Clone)"));
                    }
                }
            }
            else if(this.name == "Warrior(Clone)")
            {
                animator.SetBool("attacking", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attacking("Warrior(Clone)"));
        }
        if (playerCurrentHP <= 0)
		{
            gameObject.SetActive(false);
		}
        
    }
    IEnumerator WaitForSeconds(float time)
    {

        yield return new WaitForSeconds(time);
        gameSaveText.gameObject.SetActive(false);
        gameSave = true;
    }
    void SaveAndLoadGame()
    {
        if (gameSave)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                gameSaveText.gameObject.SetActive(true);
                gameSaveText.text = "Save Game...Please Wait";
                gameSave = false;
                playerExperience = LevelSystem.experience;
                playerLevel = LevelSystem.currentLevel;
                playerPositionX = transform.position.x;
                playerPositionY = transform.position.y;
                Debug.Log("save");
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerGold = currentGold;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerHP = playerCurrentHP;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerMana = playerCurrentMana;
                ProtectedSaveFiles.Basic.SaveController.Data.SaveCurrentEXP = LevelSystem.experience;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLevel = LevelSystem.currentLevel;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationX = transform.position.x;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationY = transform.position.y;
                ProtectedSaveFiles.Basic.SaveController.Data.SaveCharacterSelection = currentCharacterSelection;
                ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerScene = SceneManager.GetActiveScene().name;

                ProtectedSaveFiles.Basic.SaveController.Save();
                inventory.Save();
                equipment.Save();
                StartCoroutine(WaitForSeconds(1f));
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                gameSaveText.gameObject.SetActive(true);
                gameSaveText.text = "Load Game...Please Wait";
                gameSave = false;
                Debug.Log("load");
                ProtectedSaveFiles.Basic.SaveController.Load();
                currentGold = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerGold;
                playerCurrentHP = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerHP;
                playerCurrentMana = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerMana;
                playerExperience = ProtectedSaveFiles.Basic.SaveController.Data.SaveCurrentEXP;
                playerLevel = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLevel;
                playerPositionX = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationX;
                playerPositionY = ProtectedSaveFiles.Basic.SaveController.Data.SavePlayerLocationY;
                currentCharacterSelection = ProtectedSaveFiles.Basic.SaveController.Data.SaveCharacterSelection;


                transform.position = new Vector2(playerPositionX, playerPositionY);
                inventory.Load();
                equipment.Load();
                StartCoroutine(WaitForSeconds(1f));
            }
        }
    }
	void AnimationUpdate()
    {
        if (movement != Vector3.zero)
        {
            if (this.name == "Archer(Clone)" || this.name == "Mage(Clone)")
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
        if (warriorClass == "Warrior(Clone)")
        {
            animator.SetBool("attacking", true);

        }
        else if (warriorClass == "Archer(Clone)")
        {
            animator.SetBool("attacking", true);
            yield return new WaitForSeconds(archerRateOfFire);
            animator.SetBool("attacking", false);
            canFire = true;
        }
        else if (warriorClass == "Mage(Clone)")
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

        if (this.name == "Archer(Clone)")
        {

                GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, 45)));
            Debug.Log("spawn arrow");
                arrow.transform.SetParent(transform);
                arrow.GetComponent<ArrowMove>().enableUpMove = true;

        }
        else if (this.name == "Mage(Clone)")
        {

            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableUpMove = true;
        }

    }
    public void SpawnArrowLeft() {
        if (this.name == "Archer(Clone)")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, 135)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableLeftMove = true;
        }
        else if (this.name == "Mage(Clone)")
        {
            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableLeftMove = true;
        }
    }
    public void SpawnArrowRight() {
        if (this.name == "Archer(Clone)")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, -45)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableRightMove = true;
        }
        else if (this.name == "Mage(Clone)")
        {
            GameObject arrow = Instantiate(orbPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            arrow.transform.SetParent(transform);
            arrow.GetComponent<OrbMove>().enableRightMove = true;
        }
    }
    public void SpawnArrowDown() {
        if (this.name == "Archer(Clone)")
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0f, 0f, -135)));
            arrow.transform.SetParent(transform);
            arrow.GetComponent<ArrowMove>().enableDownMove = true;
        }
        else if (this.name == "Mage(Clone)")
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
            Debug.Log(groundItem);
            //initialize new item function item object
            Item newItem = new Item(groundItem.itemObject);
            if (inventory.AddItem(newItem , 1))
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
    public bool enableDxtPowr;
    public void AttributeModified(Attribute attribute)
	{
        Debug.Log(string.Concat(attribute.attributeType, " was updated! Value is now ", attribute.modifiableValue.ModifiedValue));
        var tempattackvalue = attributes[0].modifiableValue.ModifiedValue;
        var tempdefensevalue = attributes[1].modifiableValue.ModifiedValue;
        var tempintelligencevalue = attributes[2].modifiableValue.ModifiedValue;
        var tempdexterityvalue = attributes[2].modifiableValue.ModifiedValue;
        playerAttackPower = tempattackvalue;
        playerDefensePower = tempdefensevalue;
        playerIntelligencePower = tempintelligencevalue;
        playerDexterityPower = tempdexterityvalue;
        if (attribute.attributeType == Attributes.Dexterity)
        {
            if (this.name == "Archer(Clone)")
            {
                if (enableDxtPowr)
                {
                    enableDxtPowr = false;
                    float t_dexterityPower = playerDexterityPower;
                    t_dexterityPower *= multiplierPower;
                    Debug.Log(t_dexterityPower);
                    arrowRange += t_dexterityPower;
                    Debug.Log("new range " + arrowRange);
                }
            }
        }
	}
    public float arrowRange = 0.2f;
    private float multiplierPower =  0.01f;
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
