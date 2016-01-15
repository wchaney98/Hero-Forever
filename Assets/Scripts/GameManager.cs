using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Handles the spawners (for now)

    public TopAirSpawner topAirSpawner;
    public MidAirSpawner midAirSpawner;
    public BotAirSpawner botAirSpawner;
    public GroundSpawner groundSpawner;

    public float waveTime;

    Spawner[] spawners = new Spawner[4];
    float timeSinceWave;

	void Start ()
    {
        spawners[0] = topAirSpawner;
        spawners[1] = midAirSpawner;
        spawners[2] = botAirSpawner;
        spawners[3] = groundSpawner;

        timeSinceWave = 0;
	}
	
	void Update ()
    {
        timeSinceWave += Time.deltaTime;

        if (timeSinceWave >= waveTime)
        {
            timeSinceWave = 0f;
            foreach (Spawner spawner in spawners)
            {
                if (Random.Range(0f, 1f) <= spawner.spawnChancePerWave)
                {
                    spawner.spawnEnemies();
                }
            }
        }
	}
}
