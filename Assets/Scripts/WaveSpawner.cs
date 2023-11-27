using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveSpawner : MonoBehaviour
{
    public WaveData[] waves;
    public int currentWave;

    public int remainingEnemies;

    [Header("Components")] 
    public Transform enemySpawnPosition;
    public TextMeshProUGUI waveText;
    public GameObject nextWaveButton;

    public void SpawnNextWave()
    {
        currentWave++;
        
        if (currentWave - 1 == waves.Length)
            return;

        waveText.text = $"Wave: {currentWave}";

        StartCoroutine(SpawnWave());
    }
    
    public void OnEnemyDestroyed()
    {
        remainingEnemies--;

        if (remainingEnemies == 0)
            nextWaveButton.SetActive(true);

    }

    IEnumerator SpawnWave()
    {
        nextWaveButton.SetActive(false);
        var wave = waves[currentWave - 1];

        for (var x = 0; x < wave.enemySets.Length; x++)
        {
            yield return new WaitForSeconds(wave.enemySets[x].spawnDelay);

            for (var y = 0; y < wave.enemySets[x].spawnCount; y++)
            {
                SpawnEnemy(wave.enemySets[x].enemyPrefab);
                yield return new WaitForSeconds(wave.enemySets[x].spawnRate);
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, enemySpawnPosition.position, Quaternion.identity);
        remainingEnemies++;
    }
}
