using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSettings : ScriptableObject
{

	public enum BuildingType { basicBuilding , Tower, Research, UnitSpawner};

	
	[SerializeField]
	BuildingType buildingType;

	[Header("BuildingValues")]

	[SerializeField]
	[Range(0, 100000)]
	int cost;

	[SerializeField]
	[Range(1, 10000)]
	int startHitPoints;

	
	public int getCost()
	{
		return this.cost;
	}

	public int getStartHitPoints()
	{
		return this.startHitPoints;
	}





}
