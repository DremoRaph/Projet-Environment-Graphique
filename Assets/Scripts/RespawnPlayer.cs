using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    GameObject Player;
    public float TimeToRespawn = 10f;
    public float TimeToRespawnReset = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<PlayerMotor>().GetDead() == true) {
            TimeToRespawn -= Time.deltaTime;
            if (TimeToRespawn <= 0.0f)
            {
                Instantiate(Player, transform.position,Quaternion.identity);
                Destroy(Player);
            }
        }
    }
}
