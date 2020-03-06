using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    
    //Public Variables
    public GameObject prefabToSpawn;
    public float spawnTime;
    public float resetTimerWave = 10f;
    public float TimeBetweenWave;
    int MinionNumber = 5;
    int ActualPrefabNumber = 0;
    //Private Variables
    private float spawnTimer;

    //Used for initialisation
    void Start()
    {

        ResetSpawnTimer();
        TimeBetweenWave = resetTimerWave;

    }

    //Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f && ActualPrefabNumber < MinionNumber)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ResetSpawnTimer();
            ActualPrefabNumber += 1;
        }
        else if (ActualPrefabNumber == MinionNumber)
        {
            TimeBetweenWave -= Time.deltaTime;
            if(TimeBetweenWave <= 0)
            {
                ActualPrefabNumber = 0;
                TimeBetweenWave = resetTimerWave;
            }
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()
    {
        spawnTimer = (float)(spawnTime);
    }

}
