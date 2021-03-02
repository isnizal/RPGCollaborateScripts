using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Agro")]
    public Transform target;
    public float moveSpeed;
    public float chaseRadius;
    public float attackRadius;

    private Rigidbody2D myRigidbody;
    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;
    //spawn enemy name
    public GameObject enemyText;

    private Animator animator;
    private GameObject player;


    void Start()
    {

        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        if(player != null)
            target = player.transform;

        myRigidbody = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        var nameTag = Instantiate(enemyText);
        nameTag.GetComponentInChildren<Transform>().transform.position = new Vector2(transform.position.x,transform.position.y + 1f);
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = GetComponent<EnemyStats>().enemyName.ToString();
        nameTag.transform.SetParent(transform);
    }

    
    void Update()
    {
        EnemyFollow();
        if (this.gameObject.name != "RedSlime Lv1(Clone)" && this.gameObject.name != "RedSlime Lv5(Clone)")
        {
            if (myRigidbody.velocity != Vector2.zero)
            {
                animator.SetFloat("moveX", myRigidbody.velocity.x);
                animator.SetFloat("moveY", myRigidbody.velocity.y);
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }

    }

	void EnemyFollow()
	{
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            transform.position= Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
		if (Vector3.Distance(target.position, transform.position) > chaseRadius)
		{
			FreeRoam();
		}
	}

    void FreeRoam()
	{
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = moveDirection;
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }
    }        
}
