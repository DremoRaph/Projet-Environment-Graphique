using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{ 
    public BoxCollider playerWeapon;
    public GameObject Pivot;
    public float damage;
    Animator animator;
    public BoxCollider Weapon;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
}


