using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
	int attackDamages;

	float attackRange;

	float attackRate;

	
	SphereCollider attackZone;

	[SerializeField]
	List<GameObject> targetsAvaillable = new List<GameObject>();
	[SerializeField]
	int indexCurrentTarget = -1;

	public void Start()
	{
		attackDamages = ( (TowerSettings) buildingSettings ).getStartDamages();
		attackRange = ( (TowerSettings) buildingSettings ).getStartRange();
		attackRate = ( (TowerSettings) buildingSettings ).getStartRate();

		CreateAttackZone();
	}

	void CreateAttackZone()
	{
		attackZone = this.gameObject.AddComponent<SphereCollider>();
		attackZone.isTrigger = true;

		attackZone.radius = attackRange;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			targetsAvaillable.Add(other.gameObject);
			if(indexCurrentTarget == -1)
			{
				ChangeTarget();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{

			if (other.gameObject == targetsAvaillable[indexCurrentTarget])
			{
				ChangeTarget();
			}
			targetsAvaillable.Remove(other.gameObject);
		}
	}
	
	private void ChangeTarget()
	{
		if(targetsAvaillable.Count == 0)
		{
			indexCurrentTarget = -1;
			return;
		}

		float closestDistance = -1;

		int index = 0;
		foreach (var target in targetsAvaillable)
		{
			float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);

			if(targetDistance < closestDistance || closestDistance == -1)
			{
				closestDistance = targetDistance;
				this.indexCurrentTarget = index;
			}
			index ++;
		}


	}

}
