using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float timeJump;
    public float speed = 1;

    public float jumpS = 2;
    private float closeJump;

    public GameObject damageZoneLeft;
    public GameObject damageZoneRight;


    private bool roarActive;
    private bool fightActive;


    public int damagePlayer;
    public Slider sliderHp;
    public TextMeshProUGUI hpText;

    private void Start()
    {
        closeJump = 0;
    }


    public void Roar(bool x)
    {
        GetComponent<SpriteRenderer>().flipX = x;
        roarActive = true;
        GetComponent<Animator>().SetBool("RoarNow", true);
        Invoke("RoarCancel",2.1f);
    }

    public void RoarCancel()
    {
        roarActive = false;
        GetComponent<Animator>().SetBool("RoarNow", false);
    }

    public void FightNow()
    {
        GetComponent<Animator>().SetBool("Fight",true);
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            damageZoneLeft.GetComponent<Damage>().damage = damagePlayer;
            damageZoneRight.GetComponent<Damage>().damage = 0;
        }
        else
        {
            damageZoneRight.GetComponent<Damage>().damage = damagePlayer;
            damageZoneLeft.GetComponent<Damage>().damage = 0;
        }

        fightActive = true;
        Invoke("FightCancel", 3f);
    }

    public void FightCancel()
    {
        GetComponent<Animator>().SetBool("Fight", false);
        damageZoneLeft.GetComponent<Damage>().damage = 0;
        damageZoneRight.GetComponent<Damage>().damage = 0;
        fightActive = false;
    }
    private void FixedUpdate()
    {
        sliderHp.value = GetComponent<Health>().currentHealth;
        sliderHp.maxValue = GetComponent<Health>().maxHealth;
        hpText.text = GetComponent<Health>().currentHealth.ToString() + " / " + GetComponent<Health>().maxHealth.ToString();


        if (roarActive == true || fightActive == true) { return; }
        closeJump -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {   
            
            if(closeJump < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpS));
                closeJump = timeJump;
            }
           
        }

        if (Input.GetKey(KeyCode.F))
        {
            FightNow();
        }

       
        if (Input.GetKey(KeyCode.A))
        {
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                Roar(true);
                return;
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                Roar(false);
                return;
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
           
        }

        var speedMan = GetComponent<Rigidbody2D>().velocity.magnitude;
        if(speedMan <= 0)
        {
            GetComponent<Animator>().SetTrigger("Stop");

        }
        else
        {
            GetComponent<Animator>().SetTrigger("Go");
        }
    }
}
