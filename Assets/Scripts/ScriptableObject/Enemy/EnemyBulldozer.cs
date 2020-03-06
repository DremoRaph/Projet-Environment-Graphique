using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulldozer : EnenyController
{
    private GameObject[] ListBuilding;

    private void Update()
    {
        if (!Dead)
        {
            ListBuilding = GameObject.FindGameObjectsWithTag("Building");
            float distanceHeart = Vector3.Distance(targetHeart.ClosestPoint(transform.position), transform.position);
            if (ListBuilding.Length > 0)
            {
                float distanceBuilding = Vector3.Distance(transform.position, FindTargetBuilding().transform.position);

                if (distanceBuilding < distanceHeart)
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
                else
                {
                    agent.SetDestination(FindTargetBuilding().transform.position);
                    anim.SetBool("Walk Forward", true);
                    if (distanceBuilding <= agent.stoppingDistance)
                    {
                        FaceTargetBuilding();
                        AttackDelay -= Time.deltaTime;
                        if (AttackDelay <= 0.0f)
                        {
                            Attack();
                            AttackDelay = AttackReset;
                        }
                        anim.SetBool("Walk Forward", false);
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
            if (Dead)
            {
                anim.SetBool("Walk Forward", false);
                agent.isStopped = true;
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
    }



    GameObject FindTargetBuilding()
        {

            float distanceMin = 0;
            int i = 0;
            GameObject target = null;
            while (i < ListBuilding.Length)
            {
                if (distanceMin >= Vector3.Distance(this.transform.position, ListBuilding[i].transform.position) || distanceMin == 0)
                {
                    distanceMin = Vector3.Distance(this.transform.position, ListBuilding[i].transform.position);
                    target = ListBuilding[i];
                    i++;
                }
                else
                {
                    i += 1;
                }

            }
            return target;

        }
        void FaceTargetBuilding()
        {
            Vector3 direction = (FindTargetBuilding().transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            anim.SetBool("Walk Forward", false);
        }

    
    protected new void OnTriggerEnter(Collider col)
    {
        if (!Dead)
        {
            if (col.tag == ("Building") || col.tag == ("Heart"))
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
    private new void LateUpdate()
    {
        base.LateUpdate();
    }


}
