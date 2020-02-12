using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{   public float attackDuration = 1f;
    public float attackRange = 1f;
    public BoxCollider playerWeapon;
    public GameObject Pivot;
    public float damage;
    Animator animator;
   public BoxCollider Weapon;
    bool isAttacking;
    private void Start()
    {   
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttacking");
        

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
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
            col.SendMessage("TakeDamage",damage);
    }
}


