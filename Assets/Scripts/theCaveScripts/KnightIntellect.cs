using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightIntellect : MyCameraScripts
{
    public float atackRange = 4;
    public float coldownAtack = 5;
    private float currentAtack;

    public GameObject leftWeapon;
    public GameObject rightWeapon;
    public bool stopIt;
    public AudioClip clipDeath;

    public GameObject portParticle;
    public Sprite deathSprite;


    private Health healthHero;
    private int healthCurrent;

    public bool blocked;
    public float strSideBlockRoll=200;


    public void Start()
    {
        healthHero = GetComponent<Health>();
        healthCurrent = healthHero.currentHealth;
    }
    public void Dead()
    {

        GetComponent<Health>().enabled = false;
        GetComponent<Animator>().SetTrigger("Death");

        stopIt = true;
        Invoke("StaticDeath", 1f);
        GetComponent<AudioSource>().PlayOneShot(clipDeath);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(gameObject, 10f);
    }

    public void StaticDeath()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;


    }

    public void Blocked()
    {
        GetComponent<Animator>().SetTrigger("Block");
        healthHero.blockHIT = true;
        blocked = true;
        healthCurrent = healthHero.currentHealth;
        Invoke("BlockStop",0.2f);
    }

    public void BlockStop()
    {
        float sideWalk = -strSideBlockRoll;
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            sideWalk = strSideBlockRoll;
        }
        GetComponent<Animator>().SetTrigger("Roll");
        GetComponent<Rigidbody2D>().AddForce(new Vector3(sideWalk, 0, 0));
        Invoke("OpenBlock", 0.7f);
    }

    public void OpenBlock()
    {
        healthHero.blockHIT = false;
        blocked = false;
    }

    public void Attack()
    {
        float force = 125;


        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            leftWeapon.SetActive(true);
            force = -125;
        }
        else
        {
            rightWeapon.SetActive(true);
        }
        GetComponent<Animator>().SetTrigger("Attack1");
        GetComponent<Rigidbody2D>().AddForce(new Vector3(force, 0, 0));

           currentAtack = coldownAtack;
           Invoke("CloseAttack", 0.3f);
           Invoke("Attack2", 0.4f);
    }
    public void Attack2()
    {
        float force = 125;
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            leftWeapon.SetActive(true);
            force = -125;
        }
        else
        {
            rightWeapon.SetActive(true);
        }
        GetComponent<Animator>().SetTrigger("Attack2");
        GetComponent<Rigidbody2D>().AddForce(new Vector3(force, 0, 0));
        currentAtack = coldownAtack;
        Invoke("CloseAttack", 0.3f);
        Invoke("Blocked", 0.4f);

    }
    public void CloseAttack()
    {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
    }


    public void FixedUpdate()
    {
        if(stopIt == true)
        {
            return;
        }
        currentAtack -= Time.deltaTime;
        if(healthCurrent != healthHero.currentHealth)
        {
            if(blocked != true)
            {
                Blocked();
            }
            
        }

        float lenhg = (player.transform.position - gameObject.transform.position).magnitude;
        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {




            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {

            GetComponent<SpriteRenderer>().flipX = false;
        }


        if (lenhg < atackRange && currentAtack < 0)
        {
            Attack();
            return;
        }
        

        if(lenhg > atackRange && currentAtack < 0)
        {
            if(player.transform.position.x - gameObject.transform.position.x > 0)
            {
                transform.position += new Vector3(0.1f, 0, 0);
                GetComponent<Animator>().SetTrigger("r");
            }
            else
            {
                transform.position += new Vector3(-0.1f, 0, 0);
                GetComponent<Animator>().SetTrigger("r");
            }
        }

    }
}
