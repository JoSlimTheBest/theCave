using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth; //кол-во жизни
    public int maxHealth = 100;
    
    void Start()
    {
        
        currentHealth = maxHealth;
      


    }
  
    public virtual void ChangeHealth(int value)
    {

        

        currentHealth += value;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = gameObject.GetComponent<Health>();
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (health != null && damage != null)
        {

            health.ChangeHealth(-damage.GetDamage());



        }
    }

}
