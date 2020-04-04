using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
	int gold;
	int searchPoint;

	private void Start()
	{
		gold = 0;
		searchPoint = 0;
	}

	public int getCurrentGold()
	{
		return this.gold;
	}

	public int getCurrentSearchPoint()
	{
		return this.searchPoint;
	}

	public bool CanPayGold(int amount)
	{
		if (amount < gold)
		{

			return true;
		}

		return false;
	}

	public void PayGold(int amount)
	{
		this.gold -= amount;
	}

	public bool CanPaySearchPoint(int amount)
	{
		if (amount < searchPoint)
		{

			return true;
		}

		return false;
	}

	public void PaySearchPoint(int amount)
	{
		this.searchPoint -= amount;
	}




}
