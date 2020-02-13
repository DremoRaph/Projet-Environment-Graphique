using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    //Public Variables
    public GameObject prefabToSpawn;
    public float spawnTime;
    public float spawnTimeRandom;
    public int EnemyNumber;
    int i = 0;
    //Private Variables
    private float spawnTimer;

    //Used for initialisation
    void Start()
    {
        
        ResetSpawnTimer();
        
    }

    //Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f && i < EnemyNumber)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ResetSpawnTimer();
            i += 1;
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()
    {
        spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }
}