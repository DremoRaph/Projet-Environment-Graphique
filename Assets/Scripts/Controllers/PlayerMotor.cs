using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{  //Animations du perso
    Animator animator;
    //Vitesse de base

  public float walkSpeed;
  public float runSpeed;
  public float turnSpeed;

  //Inputs
  public string inputFront;
  public string inputBack;
  public string inputRight;
  public string inputLeft;
  public Vector3 jumpSpeed;

  CapsuleCollider playerCollider;


    // Start is called before the first frame update
    void Start()
    {
      animator = gameObject.GetComponent<Animator>();
      playerCollider = gameObject.GetComponent<CapsuleCollider>();

    }
    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.5f, layerMask:3);
    }
    // Update is called once per frame
    void Update()

    {
        // Si on n'avance et ne recule pas 
        if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
        {
           // animator.SetFloat("vertical", 0);
            //animator.SetFloat("horizontal", 0);
        }


        //avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)) 
            {
                transform.Translate(0,0, walkSpeed * Time.deltaTime);

                animator.SetFloat("vertical", 1);
            } 
            
         //Pour le sprint
        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
           // animations.Play("run");
        }

        //reculer
        if (Input.GetKey(inputBack)) 
        {
            transform.Translate(0,0, -(walkSpeed/2)* Time.deltaTime);
            animator.Play("Walk");

        }


      //Tourner a droite
      if (Input.GetKey(inputRight)) {
        transform.Rotate(0, turnSpeed * Time.deltaTime,0);

      }
      //Tourner a gauche
      if (Input.GetKey(inputLeft)) {
                transform.Rotate(0, -turnSpeed * Time.deltaTime,0);

      }
        // Si on saute
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Preparation du saut 
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpSpeed.y;

            // Saut
            gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
        }
    }
    
}
