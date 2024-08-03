using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToTop : MonoBehaviour
{
    public float run = 0.3f;
    public float destroych = 2f;

    public void Start()
    {
        Destroy(gameObject, 2f);
        
    }
    void FixedUpdate()
    {

        float randomich = Random.Range(-1, 1);
        transform.position += new Vector3(randomich, run, 0);
        transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        Destroy(gameObject, 1f);
    }
}
