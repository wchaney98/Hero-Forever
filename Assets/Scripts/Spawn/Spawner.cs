using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Abstract class to serve as an outline for all enemy spawners.
/// </summary>
public abstract class Spawner : MonoBehaviour
{
    public float spawnChancePerWave;
    [SerializeField]
    public GameObject[] enemies;

    /// <summary>
    /// Instantiates a random enemy in that Spawner's array of enemy objects. See GameManager class for the use of spawnChancePerWave
    /// </summary>
    public virtual void spawnEnemies()
    {
        if (enemies != null)
        {
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
