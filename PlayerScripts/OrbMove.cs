using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMove : MonoBehaviour
{
    public float orbSpeed = 1100f;
    public float selfDestroy;
    public int orbDamage = 4;
    public GameObject orbEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyOrb", selfDestroy);
    }
    public bool enableRightMove, enableUpMove, enableDownMove, enableLeftMove;
    // Update is called once per frame
    void Update()
    {
        if (enableRightMove) { 

            GetComponent<Rigidbody2D>().velocity = new Vector2(orbSpeed * Time.deltaTime, 0);
        }
        else if (enableLeftMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-orbSpeed * Time.deltaTime, 0);
        }
        else if (enableUpMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, orbSpeed * Time.deltaTime);
        }
        else if (enableDownMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -orbSpeed * Time.deltaTime);
        }

    }
    public void DestroyOrb()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(orbEffect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            collision.gameObject.GetComponent<EnemyStats>().DamageToEnemy(orbDamage);
            Destroy(gameObject);
        }
    }
}
