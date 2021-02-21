using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EffectDestroy", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EffectDestroy()
    {
        Destroy(this.gameObject);
    }
}
