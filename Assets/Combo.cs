using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator anim;
    public BoxCollider playerWeapon;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float damage = 100;
    public float maxComboDelay = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                anim.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }

    public void return1()
    {
        if (noOfClicks >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
            noOfClicks = 0;
            
        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            anim.SetBool("Attack3", true);
        }
        else
        {
            anim.SetBool("Attack2", false);
            noOfClicks = 0;
            
        }
    }

    public void return3()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        
        noOfClicks = 0;
    }
   
}
