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
        if (distancePlayer <= lookRadiusPlayer)
        {
            agent.SetDestination(targetPlayer.position);
            Debug.Log(targetPlayer);
        }
        if (distancePlayer <= agent.stoppingDistance)
        {
            FaceTargetPlayer();
        }
        if (distanceHeart <= distancePlayer)
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
}
