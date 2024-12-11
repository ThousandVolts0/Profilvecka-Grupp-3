using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WavesSystem : MonoBehaviour
{
    // List of enemy types with their respective cost
    public List<Enemy> enemyTypes = new List<Enemy>();
    public List<WaveConfig> waveConfigs = new List<WaveConfig>();

    // Current wave number and available wave value for spawning enemies
    public int currentWave;

    // List of enemies to spawn during the current wave
    private List<GameObject> enemiesToSpawn = new List<GameObject>();

    // Spawn locations for enemies
    public Transform[] spawnLocations = new Transform[4];

    private float spawnInterval;

}
[System.Serializable]
public class WaveConfig
{
    public int enemyType;
    public int enemyCount;

    public WaveConfig(int type, int count)
    {
        enemyType = type;
        enemyCount = count;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int damage = 10;
    public int maxHealth = 100;
    private int currentHealth;

    public void resetHealth()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameObject.Destroy(enemyPrefab);
        }
    }
}
