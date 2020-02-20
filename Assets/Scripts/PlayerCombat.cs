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
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
    private void LateUpdate()
    {
        hasCollide = false;
    }
}


