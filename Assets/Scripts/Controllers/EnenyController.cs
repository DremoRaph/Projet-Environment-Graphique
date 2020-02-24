using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyController : MonoBehaviour
{

    Transform targetPlayer;
    BoxCollider targetHeart;
    public float lookRadiusPlayer = 5f;
    NavMeshAgent agent;
    // Start is called before the first frame update
    public float hitpoint = 500;
    private float maxHitPoint = 500;
    Animator anim;
    float damage = 10;
    bool Dead = false;
    float DestroyTimer = 10f;
    public float AttackDelay = 1.5f;
    float AttackReset = 1.5f;
    public BoxCollider horn;


    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        targetHeart = PlayerManager.instance.heart.GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            float distancePlayer = PlayerManager.instance.player==null? float.PositiveInfinity: Vector3.Distance(targetPlayer.position, transform.position);
            float distanceHeart = Vector3.Distance(targetHeart.ClosestPoint(this.transform.position), this.transform.position);
            if (distancePlayer <= lookRadiusPlayer && distancePlayer <= distanceHeart && PlayerManager.instance.player.tag=="Player")
            {
                agent.SetDestination(targetPlayer.position);
                anim.SetBool("Walk Forward", true);
                if (distancePlayer <= agent.stoppingDistance && PlayerManager.instance.player.tag=="Player")
                {
                    FaceTargetPlayer();
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay > 1.0f)
                    {
                        Attack();
                        AttackDelay = AttackReset;
                    }
                    anim.SetBool("Walk Forward", false);
                }
            }


            else
            {
                agent.SetDestination(targetHeart.ClosestPoint(this.transform.position));
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
        Vector3 direction = (targetHeart.ClosestPoint(this.transform.position) - transform.position).normalized;
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
            damage = 10f;
            anim.SetTrigger("Stab Attack");
            hasCollide = true;
        }
        if(attack == 2)
        {
            damage = 3;
            anim.SetTrigger("Smash Attack");
            hasCollide = true;
        }
    }
    public bool hasCollide = false;
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player" || col.tag == "Heart")
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
