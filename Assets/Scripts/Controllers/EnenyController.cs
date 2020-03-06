using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyController : MonoBehaviour
{

    Transform targetPlayer;
    protected BoxCollider targetHeart;
    GameObject[] ListMinion;
    public float lookRadiusPlayer = 5f;
    protected NavMeshAgent agent;
    public float hitpoint = 100;
    protected float maxHitPoint = 100;
    protected Animator anim;
    protected float damage = 10;
    public bool Dead = false;
    protected float DestroyTimer = 10f;
    public float AttackDelay = 1.5f;
    protected float AttackReset = 1.5f;
    public BoxCollider horn;


    protected void Start()
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
            ListMinion = GameObject.FindGameObjectsWithTag("Minion");
            float distanceMinion = Vector3.Distance(this.transform.position, FindClosestMinion().transform.position);
            float distancePlayer = Vector3.Distance(targetPlayer.position, transform.position);
            float distanceHeart = Vector3.Distance(targetHeart.ClosestPoint(this.transform.position), this.transform.position);
            if (distanceMinion <= distanceHeart && distancePlayer > lookRadiusPlayer)
            {
                agent.SetDestination(FindClosestMinion().transform.position);
                anim.SetBool("Walk Forward", true);
                if (distanceMinion <= agent.stoppingDistance)
                {
                    FaceTargetMinion();
                    AttackDelay -= Time.deltaTime;
                    if (AttackDelay > 1.0f)
                    {
                        Attack();
                        AttackDelay = AttackReset;
                    }
                    anim.SetBool("Walk Forward", false);
                }
            }
            else if (distancePlayer <= lookRadiusPlayer && distancePlayer <= distanceHeart && PlayerManager.instance.player.tag=="Player")
            {
                agent.SetDestination(targetPlayer.position);
                anim.SetBool("Walk Forward", true);
                if (distancePlayer <= agent.stoppingDistance && PlayerManager.instance.player.tag=="Player")
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
                agent.SetDestination(targetHeart.ClosestPoint(transform.position));
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
                anim.SetBool("Walk Forward", false);

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
   protected void FaceTargetHeart()
    {
        Vector3 direction = (targetHeart.ClosestPoint(this.transform.position) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        anim.SetBool("Walk Forward", false);
    }
    void FaceTargetMinion()
    {
        Vector3 direction = (FindClosestMinion().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        anim.SetBool("Walk Forward", false);
    }
    protected void TakeDamage(float damage)
    {
        if (!Dead)
        {
            hitpoint -= damage;
            anim.SetTrigger("Take Damage");
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                anim.SetTrigger("Die");
                tag = "Untagged";


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
    protected void EventDestroy()
    {
        Dead = true;
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
    protected void Attack()
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
    protected void OnTriggerEnter(Collider col)
    {
        if (!Dead)
        {

            if (col.tag == "Player" || col.tag == "Heart" || col.tag == "Minion")
            {
                if (hasCollide == false)
                {
                    hasCollide = true;
                    col.SendMessage("TakeDamage", damage);
                }
            }
        }
        else
        {
            return;
        }
    }
    protected void LateUpdate()
    {
        hasCollide = false;
    }
    public GameObject FindClosestMinion()
    {
        
            float distanceMin = 0;
            int i = 0;
            GameObject target = null;
            while (i < ListMinion.Length)
            {
                if (distanceMin >= Vector3.Distance(this.transform.position, ListMinion[i].transform.position) || distanceMin == 0)
                {
                    distanceMin = Vector3.Distance(this.transform.position, ListMinion[i].transform.position);
                    target = ListMinion[i];
                    i++;
                }
                else
                {
                    i += 1;
                }

            }
            return target;
        
    }
}
