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
    public bool changeUI = true;

    private void Start()
    {
        if (changeUI)
        {
            displayNumber.text = "" + damageNumber;
            changeUI = false;
        }
        else if (!changeUI)
        {
            displayNumber.text = "" + damageCharac.ToString();
            changeUI = true;
        }
    }
    void Update()
    {

        //displayNumber.text = "" + damageNumber;
        //transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);

        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }
}
