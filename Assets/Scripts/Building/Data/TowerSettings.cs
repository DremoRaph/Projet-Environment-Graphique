using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TowerSettings : BuildingSettings
{
    new protected TowerSettings buildingSettings;

    [Header("TowerValues")]
    [SerializeField]
    [Range(0, 10000)]
    int startDamages;

    [SerializeField]
    [Range(0, 100)]
    float startRange;

    [SerializeField]
    [Range(0, 10000)]
    float startRate;

    public int getStartDamages()
    {
        return startDamages;
    }

    public float getStartRange()
    {
        return startRange;
    }

    public float getStartRate()
    {
        return startRate;
    }
}
