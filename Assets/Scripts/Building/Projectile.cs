using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float speed;
	private int damages;

	private GameObject target;

	public void setProjectileValues(float speed, int damages, GameObject target)
	{
		this.speed = speed;
		this.damages = damages;
		this.target = target;
	}

	private void OnTriggerEnter(Collider collider)
	{
		GameObject colGameObject = collider.gameObject;
		if (colGameObject == target)
		{
			colGameObject.SendMessage("TakeDamage", damages);
			Destroy(this.gameObject);
		}
	}

	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);

	}
}




