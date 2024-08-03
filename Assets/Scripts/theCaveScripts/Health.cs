using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    public int currentHealth; //кол-во жизни
    public int maxHealth = 100;


    public GameObject damageOnScreen;
    public GameObject worldCanvas;
    
    void Start()
    {
        damageOnScreen = Resources.Load<GameObject>("DamageOnScreen"); 
        worldCanvas = GameObject.Find("WorldCanvas").gameObject;
        currentHealth = maxHealth;
      


    }
  
    public virtual void ChangeHealth(int value)
    {

        

        currentHealth += value;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (currentHealth <= 0) { return; }
        Health health = gameObject.GetComponent<Health>();
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (health != null && damage != null)
        {
            if (damage.damage <= 0)
            {
                return;
            }
            GameObject damageText = Instantiate(damageOnScreen, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, worldCanvas.transform);
           
            damageText.GetComponent<TextMeshProUGUI>().text = damage.damage.ToString();

            if(damage.damage < 10)
            {
                damageText.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            if(damage.damage > 10&&damage.damage< 20)
            {
                damageText.GetComponent<TextMeshProUGUI>().color = Color.blue;
            }
          
            if(damage.damage > 20)
            {
                damageText.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            health.ChangeHealth(-damage.GetDamage());
            


        }


        if(currentHealth <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        if (GetComponent<GhostIntellect>() != null)
        {
            GetComponent<GhostIntellect>().Dead();
        }

        if (GetComponent<WitchIntellect>() != null)
        {
            GetComponent<WitchIntellect>().Dead();
        }
    }

}
