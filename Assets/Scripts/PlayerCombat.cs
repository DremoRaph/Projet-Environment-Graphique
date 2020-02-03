using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{   public float attackDuration = 1f;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public BoxCollider playerWeapon;
    public GameObject Pivot;
    private bool isAttacking;
    private GameObject origine;
    private void Start()
    {
        origine = Pivot;
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
                Debug.Log(Mathf.Rad2Deg * Pivot.transform.rotation.y);
                if (Mathf.Rad2Deg * Pivot.transform.rotation.y <= -57f)
                {
                    isAttacking = false;
                    Pivot.transform.eulerAngles = new Vector3(0, 0, 0);
                    Debug.Log(Mathf.Rad2Deg * origine.transform.rotation.y);
                    Debug.Log("a");
                }
                else
                {
                    degree -= attackDuration * Time.deltaTime;
                    Pivot.transform.Rotate(0, degree, 0);
                }
            }
            else
            {
                
            }
          
            //Attack();
        }
      
    }
    void Attack()
    { 
    }
}

