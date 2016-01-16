using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Spawner : MonoBehaviour
{
    // Abstract class to serve as an outline for all enemy spawners

    public float spawnChancePerWave;
    [SerializeField]
    public GameObject[] enemies;

    public virtual void spawnEnemies()
    {
        if (enemies != null)
        {
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
