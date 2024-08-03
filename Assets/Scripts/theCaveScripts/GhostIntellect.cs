using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIntellect : MonoBehaviour
{

    public GameObject player;
    public float atackRange = 2;
    public float moveSpeed = 3;

    public GameObject deadParticle;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Dead()
    {
        atackRange = -5;
        GetComponent<Health>().enabled = false; 
        GetComponent<Animator>().SetTrigger("Dead");
        Instantiate(deadParticle,transform.position, Quaternion.identity,null);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        float lenhg = (player.transform.position- gameObject.transform.position).magnitude;
        if(player.transform.position.x - gameObject.transform.position.x < 0)
        {


            Debug.Log(player.transform.position.x - gameObject.transform.position.x + " Lenhg");
            GetComponent<Transform>().rotation = new Quaternion(0, 180, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
       
        if (lenhg < atackRange)
        {
            GetComponent<Animator>().SetBool("Fight", true);
            GetComponent<Animator>().SetBool("Move", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Fight", false);


            GetComponent<Animator>().SetBool("Move", true);

            GetComponent<Rigidbody2D>().AddForce((player.transform.position - gameObject.transform.position) * moveSpeed);
        }
    }
}
