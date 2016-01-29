using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// From top to bottom, the first spawner. Inherits from the Spawner class.
/// </summary>
public class TopAirSpawner : Spawner
{
    /// <summary>
    /// Randomly spawns enemies. See Spawner class for the base method.
    /// </summary>
    public override void spawnEnemies()
    {
        Debug.Log("TopAirSpawner");
        //base.spawnEnemies(); commented until at least one enemy implemented here
    }

    void Start()
    {

    }

    void Update()
    {

    }
}