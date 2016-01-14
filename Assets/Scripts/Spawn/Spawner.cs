using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Spawner : MonoBehaviour
{
    public float waveTime;
    [SerializeField]
    public GameObject[] enemies;

    public abstract void spawnEnemies();
}
