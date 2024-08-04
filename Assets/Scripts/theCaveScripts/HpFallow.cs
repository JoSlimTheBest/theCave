using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpFallow : MonoBehaviour
{
    public GameObject follow;
    public Slider sliderHp;
    public TextMeshProUGUI hpText;
    private GameObject sl;


    public void Start()
    {
        sliderHp = GetComponent<Slider>();
        hpText = sliderHp.GetComponentInChildren<TextMeshProUGUI>();
        sliderHp.maxValue = follow.GetComponent<Health>().maxHealth;
    }
    public void DestrDie()
    {
        Destroy(gameObject);
    }
    public void FixedUpdate()
    {
        if (follow == null)
        { DestrDie();
            return;
        }

        if(follow.GetComponent<Health>().currentHealth <= 0)
        {
            DestrDie();
        }

        sliderHp.transform.position = Camera.main.WorldToScreenPoint(follow.transform.position) + new Vector3(0, 75, 0);



        sliderHp.value = follow.GetComponent<Health>().currentHealth;
       
        hpText.text = follow.GetComponent<Health>().currentHealth.ToString() + " / " + follow.GetComponent<Health>().maxHealth.ToString();
    }
}
