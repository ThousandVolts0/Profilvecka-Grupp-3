using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class WavesSystem : MonoBehaviour
{
    // List of enemy types with their respective cost
    public List<Enemy> enemyTypes = new List<Enemy>();
    public List<WaveConfig> waveConfigs = new List<WaveConfig>();

    // Current wave number and available wave value for spawning enemies
    public int currentWave;
    public GameObject waveText;

    // List of enemies to spawn during the current wave
    private List<GameObject> enemiesToSpawn = new List<GameObject>();

    // Spawn locations for enemies
    public Transform[] spawnLocations = new Transform[8];

    public List<GameObject> activeEnemies = new List<GameObject>();

    public Transform player1Pos;
    public Transform player2Pos;

    private bool hasSpawnedRound = false;
    public EnemyBehaviour enemyScript;

    private void Start()
    {
        StartCoroutine(GenerateWave(waveConfigs[currentWave]));
    }

    public IEnumerator DisplayWave(int currentWave)
    {
        var text = waveText.GetComponent<TextMeshProUGUI>();
        int visualizedWave = currentWave + 1;
        text.text = "Wave " + visualizedWave;
        for (int i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(0.0075f);
            float alpha = i / 255f;
            text.color = new Color(1f, 1f, 1f, alpha);
        }
        for (int i = 255; i > 0; i--)
        {
            yield return new WaitForSeconds(0.0075f);
            float alpha = i / 255f;
            text.color = new Color(1f, 1f, 1f, alpha);
        }
    }

    public IEnumerator GenerateWave(WaveConfig config)
    {
        Debug.Log("Started wave " + currentWave);

        if (config == null) yield break;

        StartCoroutine(DisplayWave(currentWave));

        hasSpawnedRound = false;

        for (int i = 0; i < config.enemies.Length; i++)
        {
            if (enemyTypes[i] != null)
            {
                for (int j = 0; j < config.enemyCount[i]; j++)
                {
                    yield return new WaitForSeconds(config.spawnInterval);

                    int spawnLocNumber = Random.Range(0, spawnLocations.Length);
                    Transform spawnLoc = spawnLocations[spawnLocNumber];

                    if (spawnLoc != null)
                    {
                        GameObject enemyObject = Instantiate(enemyTypes[i].enemyPrefab, spawnLoc.position, Quaternion.identity);
                        activeEnemies.Add(enemyObject);

                        Enemy enemy = new Enemy();
                        enemy.initialize(this);
                        enemy.enemyPrefab = enemyObject;
                        enemy.speed = enemyTypes[i].speed;
                        enemy.maxHealth = enemyTypes[i].maxHealth;
                        enemy.damage = enemyTypes[i].damage;
                        enemy.resetHealth();

                        enemyScript = enemyObject.AddComponent<EnemyBehaviour>();
                        enemyScript.intializeEnemy(enemy);
                    }
                    else
                    {
                        Debug.LogWarning("Spawn location of " + spawnLoc + " does not exist.");
                    }
                }
            }
        }

        hasSpawnedRound = true;
    }

    public void CheckWaveCompletion()
    {
        if (activeEnemies.Count == 0)
        {
            if (hasSpawnedRound)
            {
                currentWave++;
                if (currentWave < waveConfigs.Count)
                {
                    StartCoroutine(GenerateWave(waveConfigs[currentWave]));
                }
                else
                {
                    Debug.Log("All waves are completed.");
                }
            }
            else
            {
                Debug.LogWarning("Current wave hasn't finished spawning yet!");
            }
        }
    }
}

[System.Serializable]
public class WaveConfig
{
    public int[] enemies;
    public int[] enemyCount;
    public float spawnInterval;

    public WaveConfig(int[] type, int[] count, int interval)
    {
        enemies = type;
        enemyCount = count;
        spawnInterval = interval;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    WavesSystem wavesSystem;
    public int damage = 10;
    public int maxHealth = 100;
    public float speed = 3f;
    private int currentHealth;
    private bool isDead = false;
    public bool isTouching = false;

    public void initialize(WavesSystem system)
    {
        wavesSystem = system;
        resetHealth();
    }

    public void resetHealth()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            die();
        }
    }

    public void die()
    {
        isDead = true;
        GameObject.Destroy(enemyPrefab);
        wavesSystem.activeEnemies.Remove(enemyPrefab);
        wavesSystem.CheckWaveCompletion();
    }

    public void Update()
    {
        if (!isDead && isTouching == false)
        {
            Transform player1Pos = wavesSystem.player1Pos;
            Transform player2Pos = wavesSystem.player2Pos;

            float enemyDistanceP1 = Vector3.Distance(player1Pos.position, enemyPrefab.transform.position);
            float enemyDistanceP2 = Vector3.Distance(player2Pos.position, enemyPrefab.transform.position);

            Move(player1Pos, player2Pos, enemyDistanceP1, enemyDistanceP2);
        }
    }

    private void Move(Transform p1Pos, Transform p2Pos, float distanceP1, float distanceP2)
    {
            Vector3 targetPos = (distanceP1 < distanceP2) ? p1Pos.position : p2Pos.position;
            Vector3 smoothPos = Vector3.MoveTowards(enemyPrefab.transform.position, targetPos, speed * Time.deltaTime);
            enemyPrefab.transform.position = smoothPos;
    }
}
