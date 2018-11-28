using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    static Animator anim;
    

     void Start()
    {
        anim = GetComponent<Animator>();
    }

     void Update()
    {



        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);


        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
        }



        if (translation != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);


        }
        else anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);


        if (translation != 0 && Input.GetKey("left shift"))
        {
            
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);

            
        }
        else
        {
            anim.SetBool("isRunning", false);
            

        }



    }

        }



