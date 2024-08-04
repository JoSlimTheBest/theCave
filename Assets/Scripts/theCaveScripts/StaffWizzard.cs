using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffWizzard : MonoBehaviour
{
    public bool rightWeapon;

    public float strForce = 100;
    public float strTop = 300;

    public void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>() != null)
        {
            float sideStrenght = strForce;
            if (rightWeapon == false) { sideStrenght = -strForce; }
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(sideStrenght, strTop, 0));

            Debug.Log("STaff");
        }
    }
}
