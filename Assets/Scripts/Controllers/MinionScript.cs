using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionScript : MonoBehaviour
{
    GameObject [] ListEnemy;
    NavMeshAgent agent;
    public float hitpoint = 500;
    private float maxHitPoint = 500;
    Animator anim;
    float damage = 100;
    bool Dead = false;
    float DestroyTimer = 10f;
    public float AttackDelay = 1.5f;
    float AttackReset = 1.5f;
    int range = 30;
    bool hasCollide = false;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ListEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        agent.SetDestination(FindTarget().transform.position);
        anim.SetBool("Run", true);
        if( Vector3.Distance(FindTarget().transform.position,transform.position) <= agent.stoppingDistance)
        {
            anim.SetBool("Run", false);
            FaceTarget();
            anim.SetTrigger("Shoot");
            Shoot();
            
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
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
        {
            Debug.Log(hit);
            if (hasCollide == false)
            {
                hasCollide = true;
                hit.collider.GetComponentInParent<EnenyController>().SendMessage("TakeDamage",damage);

            }
        }
    }
}
