using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles game events. Currently only handles enemy spawning.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Spawners
    public TopAirSpawner topAirSpawner;
    public MidAirSpawner midAirSpawner;
    public BotAirSpawner botAirSpawner;
    public GroundSpawner groundSpawner;

    // Spawners use spawnEnemies function every waveTime seconds
    public float waveTime;

    // GameObject of HUD for Player Health (text)
    public GameObject HudPlayerHealthObj;
    Text hudPlayerHealth;

    // Array for Spawners
    Spawner[] spawners = new Spawner[4];

    // Float to store elapsed time
    float timeSinceWave;

    // Public initial player health value
    public float startingPlayerHealth;

    // Declare PlayerState struct
    public PlayerState playerState;

	void Start ()
    {
        // Assign spawners to spawners array
        spawners[0] = topAirSpawner;
        spawners[1] = midAirSpawner;
        spawners[2] = botAirSpawner;
        spawners[3] = groundSpawner;

        // Initialize HUDPlayerHealth text object
        hudPlayerHealth = HudPlayerHealthObj.GetComponent<Text>();

        // Initialize wave-time to 0
        timeSinceWave = 0;

        // Initialize PlayerState struct
        playerState = new PlayerState(startingPlayerHealth);
    }
	
	void Update ()
    {
        // Increments time elapsed every frame
        timeSinceWave += Time.deltaTime;

        // Takes enemy spawn chance from child Spawner uses rand to see if that Spawner will spawn this wave
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
        // End of spawning code

        UpdateHUD();
	}

    /// <summary>
    /// Updates the Heads-Up-Display
    /// </summary>
    void UpdateHUD()
    {
        hudPlayerHealth.text = "HEALTH: " + playerState.health;
    }
}
