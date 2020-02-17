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
    private float hitpoint = 100;
    private float maxHitPoint = 100;
    
    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        targetHeart = PlayerManager.instance.heart.transform;
        agent = GetComponent<NavMeshAgent>();
    }



    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector3.Distance(targetPlayer.position, transform.position);
        float distanceHeart = Vector3.Distance(targetHeart.position, transform.position);
        if (distancePlayer <= lookRadiusPlayer && distancePlayer <= distanceHeart)
        {
            agent.SetDestination(targetPlayer.position);
            Debug.Log(targetPlayer);
        }
        else if (distancePlayer <= agent.stoppingDistance)
        {
            FaceTargetPlayer();
        }

        else
        {
            agent.SetDestination(targetHeart.position);
            FaceTargetHeart();
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
        transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void FaceTargetHeart()
    {
        Vector3 direction = (targetHeart.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void TakeDamage(float damage)
    {
        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            hitpoint = 0;
            Destroy(gameObject);
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
}
