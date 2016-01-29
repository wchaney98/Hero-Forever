using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// From top to bottom, the second spawner. Inherits from the Spawner class.
/// </summary>
public class MidAirSpawner : Spawner
{
    /// <summary>
    /// Randomly spawns enemies. See Spawner class for the base method.
    /// </summary>
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
