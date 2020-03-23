using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected  BuildingSettings buildingSettings;

    int maxHitPoints;
    int currentHitPoints;

    // Start is called before the first frame update
    protected void Start()
    {
        this.maxHitPoints = buildingSettings.getStartHitPoints();
        this.currentHitPoints = buildingSettings.getStartHitPoints();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public int getBuildingCost()
    {
        return buildingSettings.getCost();
    }  

    void OnDestroy()
    {
        //animation ou fade out
    }

    public void TakeDamage(int damage)
    {
        this.currentHitPoints -= damage;
        if (this.currentHitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
