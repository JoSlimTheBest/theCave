using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public int currentHealth; //кол-во жизни
    public int maxHealth = 100;


    private GameObject damageOnScreen;
    private GameObject worldCanvas;
    public bool blockHIT;

    public GameObject sliderInsta;

    public Slider sliderHp;
    public TextMeshProUGUI hpText;
    private GameObject sl;

    
    void Start()
    {
        damageOnScreen = Resources.Load<GameObject>("DamageOnScreen"); 
        worldCanvas = GameObject.Find("WorldCanvas").gameObject;
        currentHealth = maxHealth;
      

        if(gameObject.name != "Player")
        {
            sliderInsta = Resources.Load<GameObject>("SliderHpEnemy");
            sl =  Instantiate(sliderInsta, worldCanvas.transform);
            sl.GetComponent<HpFallow>().follow = gameObject;
           
        }


    }
  
    public virtual void ChangeHealth(int value)
    {

        

        currentHealth += value;



        GameObject damageText = Instantiate(damageOnScreen, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, worldCanvas.transform);

        int valStr = -value;
        damageText.GetComponent<TextMeshProUGUI>().text = valStr.ToString();

        Debug.Log("Value" + valStr.ToString());
        if (valStr > 10)
        {
            damageText.GetComponent<TextMeshProUGUI>().color = new Color(1, 0.8f, 0);
        }
        if (valStr > 10 && valStr < 20)
        {
            damageText.GetComponent<TextMeshProUGUI>().color = new Color(1, 0.5f, 0);
        }

        if (valStr > 20)
        {
            damageText.GetComponent<TextMeshProUGUI>().color = new Color(1,0,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (currentHealth <= 0 || blockHIT == true) { return; }
        Health health = gameObject.GetComponent<Health>();
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (health != null && damage != null)
        {
            if (damage.damage <= 0)
            {
                return;
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

        if(gameObject.name != "Player") { sl.GetComponent<HpFallow>().DestrDie(); }
       
        if (GetComponent<GhostIntellect>() != null)
        {
            GetComponent<GhostIntellect>().Dead();
        }

        if (GetComponent<WitchIntellect>() != null)
        {
            GetComponent<WitchIntellect>().Dead();
        }

        if (GetComponent<WizzardIntellect>() != null)
        {
            GetComponent<WizzardIntellect>().Dead();
        }
        if (GetComponent<KnightIntellect>() != null)
        {
            GetComponent<KnightIntellect>().Dead();
        }
    }



  
}
