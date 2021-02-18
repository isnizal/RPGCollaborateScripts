using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    public float moveSpeed;
    public int damageNumber;
    public string damageCharac;
    public Text displayNumber;
    public bool changeUI;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (changeUI)
        {
            displayNumber.text = "" + damageNumber;
        }
        else if (!changeUI)
        {
            displayNumber.text = "" + damageCharac;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }
}
