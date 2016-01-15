using UnityEngine;
using System.Collections;
using System;

public class MidAirSpawner : Spawner
{
    // "MidAir" enemy spawner

    public override void spawnEnemies()
    {
        Debug.Log("MidAirSpawner");

        if (enemies != null)
        {
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length - 1)], transform.position, Quaternion.identity);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
