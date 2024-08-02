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
        Health health = gameObject.GetComponent<Health>();
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (health != null && damage != null)
        {
            GameObject damageText = Instantiate(damageOnScreen, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, worldCanvas.transform);
            damageText.GetComponent<TextMeshProUGUI>().text = damage.damage.ToString();
            health.ChangeHealth(-damage.GetDamage());
            


        }
    }


}
