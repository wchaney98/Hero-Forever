using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Spawner : MonoBehaviour
{
    // Abstract class to serve as an outline for all enemy spawners

    public float spawnChancePerWave;
    [SerializeField]
    public GameObject[] enemies;

    public abstract void spawnEnemies();
}
