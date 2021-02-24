using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public float arrowSpeed = 1100f;
    private float shootingRange ;
    public int arrowDamage = 2;
    Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        shootingRange = thePlayer.arrowRange;
        Invoke("DestroyArrow", shootingRange);
    }
    public bool enableRightMove, enableUpMove, enableDownMove, enableLeftMove;
    // Update is called once per frame
    void Update()
    {
        if (enableRightMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * Time.deltaTime, 0);
        }
        else if (enableLeftMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed * Time.deltaTime, 0);
        }
        else if (enableUpMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, arrowSpeed * Time.deltaTime);
        }
        else if (enableDownMove)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -arrowSpeed * Time.deltaTime);
        }
        
    }
    public void DestroyArrow()
    {
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyStats>().DamageToEnemy(arrowDamage);
            Destroy(gameObject);
        }
    }
}
