using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float timeJump;
    public float speed = 1;

    public float jumpS = 2;
    private float closeJump;


    private bool roarActive;

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
    private void FixedUpdate()
    {
        if (roarActive == true) { return; }
        closeJump -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {   
            
            if(closeJump < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpS));
                closeJump = timeJump;
            }
           
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
