using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyController : MonoBehaviour
{

    Transform targetPlayer;
    Transform targetHeart;
    public float lookRadiusPlayer = 5f;
    NavMeshAgent agent;
    // Start is called before the first frame update
    private float hitpoint = 500;
    private float maxHitPoint = 500;
    Animator anim;
    bool Dead = false;
    float DestroyTimer = 10f;
    public float AttackDelay = 1.5f;
    float AttackReset = 1.5f;


    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        targetHeart = PlayerManager.instance.heart.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            float distancePlayer = Vector3.Distance(targetPlayer.position, transform.position);
            float distanceHeart = Vector3.Distance(targetHeart.position, transform.position);
            if (distancePlayer <= lookRadiusPlayer && distancePlayer <= distanceHeart)
            {
                agent.SetDestination(targetPlayer.position);
                anim.SetBool("Walk Forward", true);
                if (distancePlayer <= agent.stoppingDistance)
                {
                    FaceTargetPlayer();
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay <= 0.0f)
                    {
                        Attack();
                        AttackDelay = AttackReset;
                    }
                    anim.SetBool("Walk Forward", false);
                }
            }


            else
            {
                agent.SetDestination(targetHeart.position);
                anim.SetBool("Walk Forward", true);
                if (distanceHeart <= agent.stoppingDistance)
                {
                    FaceTargetHeart();
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay <= 0.0f)
                    {
                        Attack();
                        AttackDelay = AttackReset;
                    }
                    anim.SetBool("Walk Forward", false);
                }

            }
        }
        else
        {
            DestroyTimer -= Time.deltaTime;
            if (DestroyTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadiusPlayer);
    }
    void FaceTargetPlayer()
    {
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        anim.SetBool("Walk Forward", false);
    }
    void FaceTargetHeart()
    {
        Vector3 direction = (targetHeart.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        anim.SetBool("Walk Forward", false);
    }
    private void TakeDamage(float damage)
    {
        if (!Dead)
        {
            hitpoint -= damage;
            anim.SetTrigger("Take Damage");
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                anim.SetTrigger("Die");


            }
        }
        else
        {
            return;
        }

    }

    private void HealDamage(float heal)
    {
        hitpoint += heal;
        if (hitpoint > maxHitPoint)
        {
            hitpoint = maxHitPoint;
        }
    }
    private void EventDestroy()
    {
        Dead = true;
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
    }
    private void Attack()
    {
        int attack = Random.Range(0, 3);
        if(attack == 1)
        {
            anim.SetTrigger("Stab Attack");
        }
        if(attack == 2)
        {
            anim.SetTrigger("Smash Attack");
        }
    }
}
