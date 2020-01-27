using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyController : MonoBehaviour
{

    Transform targetPlayer;
    public float lookRadiusPlayer = 10f;
    NavMeshAgent agent;
    // Start is called before the first frame update

    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }



    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(targetPlayer.position, transform.position);

        if (distance <= lookRadiusPlayer)
        {
            agent.SetDestination(targetPlayer.position);
            Debug.Log(targetPlayer);
        }
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadiusPlayer);
    }
    void FaceTarget()
    {
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
