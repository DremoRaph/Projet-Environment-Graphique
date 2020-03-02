using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MinionScript : MonoBehaviour
{
    GameObject [] ListEnemy;
    NavMeshAgent agent;
    public float hitpoint = 500;
    private float maxHitPoint = 500;
    Animator anim;
    public bool CollisionWithMinion;
    float damage = 100;
    bool Dead = false;
    float DestroyTimer = 10f;
    public float AttackDelay = 1.5f;
    float AttackReset = 1.5f;
    int range = 30;
    bool hasCollide = false;
    Vector3 heart;
   
    // Start is called before the first frame update
    void Start()
    {
        heart = PlayerManager.instance.heart.GetComponent<BoxCollider>().ClosestPoint(transform.position);
        anim = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        ListEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (ListEnemy.Length > 0)
        {
            agent.SetDestination(FindTarget().transform.position);
            anim.SetBool("Run", true);
            if (Vector3.Distance(FindTarget().transform.position, transform.position) <= agent.stoppingDistance)
            {
                anim.SetBool("Run", false);
                FaceTarget();
                anim.SetTrigger("Shoot");
                Shoot();

            }
        }
        else
        {

            anim.SetBool("Run", true);
            FaceTargetHeart();
            agent.SetDestination(heart);
            
            if (Vector3.Distance(transform.position, heart) <= agent.stoppingDistance)
            {   
                agent.isStopped = true;
                anim.SetBool("Run", false);
            }
        }

        if(agent.velocity.magnitude < 0.1f)
        {
            anim.SetBool("Run", false);
        }
    }
    private void LateUpdate()
    {
        hasCollide = false;
    }
    public GameObject FindTarget()
    {
        float distanceMin = 0;
        int i = 0;
        GameObject target = null;
        while (i < ListEnemy.Length)
        {
            if (distanceMin >= Vector3.Distance(this.transform.position, ListEnemy[i].transform.position) || distanceMin == 0)
            {
                distanceMin = Vector3.Distance(this.transform.position, ListEnemy[i].transform.position);
                target = ListEnemy[i];
                i++;
            }
            else
            {
                i += 1;
            }
            
        }
        return target;
    }
    void FaceTarget()
    {
        Vector3 direction = (FindTarget().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out hit, range, LayerMask.GetMask("Enemy")))
        {
            Debug.Log(hit);
            if (hasCollide == false)
            {
                hasCollide = true;
                hit.collider.GetComponentInParent<EnenyController>().SendMessage("TakeDamage", damage);

            }
        }
    }
    void FaceTargetHeart()
    {
        Vector3 direction = (heart - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            CollisionWithMinion = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    { if (collision.gameObject.tag == "Minion")
        {
            CollisionWithMinion = false;
        }
    }

}
