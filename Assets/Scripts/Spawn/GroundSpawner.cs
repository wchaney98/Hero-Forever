using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// From top to bottom, the fourth spawner. Inherits from the Spawner class.
/// </summary>
public class GroundSpawner : Spawner
{
    /// <summary>
    /// Randomly spawns enemies. See Spawner class for the base method.
    /// </summary>
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