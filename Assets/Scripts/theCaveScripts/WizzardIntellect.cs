using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WizzardIntellect : PLayerCondition
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


    public void Dead()
    {

        GetComponent<Health>().enabled = false;
        GetComponent<Animator>().SetTrigger("Dead");

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

    public void Attack(bool leftAtackSide)
    {
        GetComponent<Animator>().SetTrigger("Atack");
        if(leftAtackSide == true)
        {
            leftWeapon.SetActive(true);
        }
        else
        {
            rightWeapon.SetActive(true);
        }
  
        currentAtack = coldownAtack;
        Invoke("CloseAttack", 1.2f);
    }
    public void CloseAttack()
    {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
    }

    public void Teleport()
    {
        int addPort = 4;
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            addPort = -4;
        }
        GameObject port1 =Instantiate(portParticle, transform.position, Quaternion.identity);
        port1.GetComponent<AutoDestroy>().timeDie = 1;


        transform.position = new Vector3(player.transform.position.x + addPort, transform.position.y, 0);

        currentAtack = coldownAtack;
        GameObject port2 = Instantiate(portParticle, transform.position, Quaternion.identity);
        port2.GetComponent<AutoDestroy>().timeDie = 2;
    }
    public void FixedUpdate()
    {
        if (stopIt == true) { return; }
        currentAtack -= Time.deltaTime;
        float lenhg = (player.transform.position - gameObject.transform.position).magnitude;
        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {




            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {

            GetComponent<SpriteRenderer>().flipX = false;
        }


        if (lenhg < atackRange && currentAtack <0)
        {
            if (player.transform.position.x - gameObject.transform.position.x < 0)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }

            return;
        }

        if(currentAtack < 0)
        {
            Teleport();
        }
    }
}
