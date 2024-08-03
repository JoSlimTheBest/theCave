using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchIntellect : MonoBehaviour
{
    public GameObject player;
    public GameObject ghost;

    public float strenghJump = 200;
    public float strenghSide = 1;
    public float timeJump = 10; 
    public float timeSummon = 12;
    private float currentJump = 10;
    private float currentSummon = 3;

    public Sprite deathSprite;


    private bool stopIt;

    
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Dead()
    {
        
        GetComponent<Health>().enabled = false;
        GetComponent<Animator>().SetTrigger("Dead");

        stopIt = true;
        Invoke("StaticDeath",2f);
        Destroy(gameObject,10f);
    }

    public void StaticDeath()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }


    public void JumpNow()
    {
        float sideJ = strenghSide;
        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {
            sideJ = -strenghSide;
        }
        

        GetComponent<Animator>().SetTrigger("Jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector3(sideJ, strenghJump));
        currentJump = timeJump;
    }


    public void Summon()
    {


        
        GetComponent<Animator>().SetTrigger("Summon");
        Invoke("InstaSum", 1f);
        currentSummon = timeSummon;
    }

    public void InstaSum()
    {
        float sideJ = 2;
        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {
            sideJ = -2;
        }
        Instantiate(ghost, gameObject.transform.position + new Vector3(sideJ, 0, 0), Quaternion.identity);
    }


    private void FixedUpdate()
    {

      if(  stopIt == true) { return; }
       currentJump -= Time.deltaTime;
       currentSummon -= Time.deltaTime;

        if (currentJump < 0)
        {
            JumpNow();
        }
        if (currentSummon < 0)
        {
            Summon();
        }


        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {


         
            
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
           
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


}
