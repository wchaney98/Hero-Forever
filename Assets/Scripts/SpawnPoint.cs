using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    public GameObject enemyToSpawnPrefab;     // turn into array of enemies?
    public float waveTime;

    private float timeSinceLastSpawn;

    void Awake ()
    {
        if (enemyToSpawnPrefab == null)
            Debug.LogError("Missing enemyToSpawnPrefab!");

        timeSinceLastSpawn = 0;
	}
	
	void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= waveTime)
        {
            Instantiate(enemyToSpawnPrefab, transform.position, Quaternion.identity);
            timeSinceLastSpawn = 0;
        }    
	}
}
