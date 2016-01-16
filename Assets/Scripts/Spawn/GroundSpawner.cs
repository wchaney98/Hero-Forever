using UnityEngine;
using System.Collections;
using System;

public class GroundSpawner : Spawner
{
    // "Ground" enemy spawner

    public override void spawnEnemies()
    {
        Debug.Log("GroundSpawner");
        base.spawnEnemies();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}