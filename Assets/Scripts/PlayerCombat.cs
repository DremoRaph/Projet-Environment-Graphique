using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{   public float attackDuration = 1f;
    public float attackRange = 1f;
    public BoxCollider playerWeapon;
    public GameObject Pivot;
    private bool isAttacking;
    bool isDamaging = true;
    public float damage;
   CapsuleCollider playerCollider;
    private void Start()
    {
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
           
        }

        if (isAttacking)
        {
            float degree = 0f;

            if (isAttacking)
            {

                if (Mathf.Rad2Deg * Pivot.transform.localRotation.y <= -57f)
                {
                    isAttacking = false;
                    Pivot.transform.localEulerAngles = new Vector3(0, playerCollider.transform.rotation.y, 0);

                }
                else
                {
                    degree -= attackDuration * Time.deltaTime;
                    Pivot.transform.Rotate(0, degree, 0, Space.Self);
                    
                }
            }
          
           
        }
      
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
            col.SendMessage("TakeDamage",damage);
    }
}


