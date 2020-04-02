using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
	int attackDamages;

	float attackRange;
	SphereCollider attackZone;

	float attackRate;
	float timeBeforeAttack = 0f;

	float projectileSpeed;

	[SerializeField]
	Transform attackStartPoint;

	GameObject projectilePrefab;

	List<GameObject> targetsAvaillable = new List<GameObject>();
	int indexCurrentTarget = -1;



	[SerializeField]
	GameObject buildingZone;
	[SerializeField]
	GameObject pivotBase;
	[SerializeField]
	GameObject pivotGun;

	public void Start()
	{
		attackDamages = ( (TowerSettings) buildingSettings ).getStartDamages();
		attackRange = ( (TowerSettings) buildingSettings ).getStartRange();
		attackRate = ( (TowerSettings) buildingSettings ).getStartRate();
		projectileSpeed = ( (TowerSettings) buildingSettings ).getProjectileSpeed();
		projectilePrefab = ( (TowerSettings) buildingSettings ).getProjectilePrefab(); ;

		CreateAttackZone();

		buildingZone.SetActive(false);
	}

	private void Update()
	{
		if (indexCurrentTarget != -1)
		{
			pivotBase.transform.LookAt(targetsAvaillable[indexCurrentTarget].transform);
			pivotBase.transform.eulerAngles = new Vector3(0, pivotBase.transform.eulerAngles.y, 0);

			pivotGun.transform.LookAt(targetsAvaillable[indexCurrentTarget].transform);
			pivotGun.transform.eulerAngles = new Vector3(pivotGun.transform.eulerAngles.x, 0, 0);
		}
	}

	private void FixedUpdate()
	{
		timeBeforeAttack -= Time.deltaTime;

		if (timeBeforeAttack <= 0f && indexCurrentTarget != -1)
		{
			Attack();
		}
	}

	void Attack()
	{
		timeBeforeAttack = attackRate;

		GameObject projectile = 
			Instantiate(projectilePrefab, attackStartPoint.position, attackStartPoint.rotation);

		projectile.GetComponent<Projectile>()
			.setProjectileValues(projectileSpeed, attackDamages, targetsAvaillable[indexCurrentTarget]);

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
