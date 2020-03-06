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


    public float hitpoint = 100;
    public float maxHitPoint = 100;
    bool Dead = false;
    public BoxCollider playerWeapon;
    public GameObject Pivot;
    public float damage;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;
    public GameObject RespawnPoint;
    public float TimeToRespawn = 10f;
    public float TimeToRespawnReset = 10f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    float pitch = 0.0f;
    float yawn = 0.0f;
    public GameObject cam;



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
        if (!Dead)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("Strafe Droite", false);
            animator.SetBool("Strafe Gauche", false);
            // Si on n'avance et ne recule pas 
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0);
            }


            //avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);

                animator.SetFloat("vertical", 1);
            }

            //Pour le sprint
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animator.SetBool("IsRunning", true);
            }

            //reculer
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                animator.SetFloat("horizontal", 2);
                animator.SetFloat("vertical", 1);

            }
             //Bouger a droite
             if(Input.GetKey(inputRight) && !Input.GetKey(KeyCode.LeftShift))
             {
                transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
                animator.SetBool("Strafe Droite", true);
             }
             if(Input.GetKey(inputLeft) && !Input.GetKey(KeyCode.LeftShift)) 
            {
                transform.Translate(-(walkSpeed * Time.deltaTime), 0, 0);
                animator.SetBool("Strafe Gauche", true);
            }

            //Tourner la camera
            
            yawn += speedH * Input.GetAxis("Mouse X");
            pitch += speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(0, yawn, 0);
            
            cam.transform.eulerAngles = new Vector3(- Mathf.Clamp(pitch,-30,30), yawn, 0);
            

            

            // Si on saute
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                // Preparation du saut 
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                // Saut
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
                animator.SetTrigger("IsJumping");
            }
            if (Input.GetMouseButtonDown(0))
            {
                lastClickedTime = Time.time;
                noOfClicks++;
                if (noOfClicks == 1)
                {
                    animator.SetBool("Attack1", true);
                }
                noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
            }

            if (Input.GetMouseButtonDown(1))
            {

                animator.SetTrigger("HighSpin");
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                animator.SetTrigger("IsCasting");
            }

        }
        else
        {
            if (Dead)
            {
                TimeToRespawn -= Time.deltaTime;
                if (TimeToRespawn <= 0.0f)
                {

                    EventRevive();
                }
            }
        }
    }
    private void TakeDamage(float damage)
    {
            hitpoint -= damage;
            animator.SetTrigger("Take Damage");
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                animator.SetTrigger("Death");
            


            }
    }
    private void EventDeath()
    {
        Dead = true;
        tag = "Untagged";
        GetComponent<Rigidbody>().useGravity = false;
        playerCollider.isTrigger = true;
    }
    private void EventRevive()
    {
        Dead = false;
        hitpoint = maxHitPoint;
        this.tag = "Player";
        this.GetComponent<Rigidbody>().useGravity = true;
        playerCollider.isTrigger = false;
        this.transform.position = RespawnPoint.transform.position;
        animator.Play("idle");
        TimeToRespawn = TimeToRespawnReset;
    }
    private void HealDamage(float heal)
    {
        hitpoint += heal;
        if (hitpoint > maxHitPoint)
        {
            hitpoint = maxHitPoint;
        }
    }
    private bool hasCollide = false;
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Enemy")
        {
            if (hasCollide == false)
            {
                hasCollide = true;
                col.SendMessage("TakeDamage", damage);
            }
        }
    }
    public void return1()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
            noOfClicks = 0;

        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
            noOfClicks = 0;

        }
    }

    public void return3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);

        noOfClicks = 0;
    }

    private void LateUpdate()
    {
        hasCollide = false;
    }
    public bool GetDead()
    {
        return this.Dead;
    }
}
