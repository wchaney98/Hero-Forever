using UnityEngine;
using System.Collections;
using System;

public class MidAirSpawner : Spawner
{
    // "MidAir" enemy spawner

    public override void spawnEnemies()
    {
        Debug.Log("MidAirSpawn");
        base.spawnEnemies();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
