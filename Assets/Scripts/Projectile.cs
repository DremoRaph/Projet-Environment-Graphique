using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public int damages;
	public GameObject target;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == target)
		{

			Destroy(this.gameObject);
		}
	}
}
