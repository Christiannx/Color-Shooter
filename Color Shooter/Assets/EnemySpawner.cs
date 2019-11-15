using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] Enemy[] enemies;
    [SerializeField] float spawnDistance;
    [SerializeField] float spawnTime;

    Transform player;
    int waveCount = 0;
    int enemiesToSpawn = 3;
    float bossHealthIncrement;
    bool waveInitiated = false;

    void Start() {
        player = FindObjectOfType<Player>().transform;
    }

    void Update() {
        if (!waveInitiated)
            StartCoroutine(nameof(NextWave));
    }

    IEnumerator NextWave() {
        waveInitiated = true;
        waveCount++;
        enemiesToSpawn++;

        for (int i = 0; i < enemiesToSpawn; i++) {
            yield return new WaitForSeconds(spawnTime);   
            SpawnEnemy();
        }

        yield return new WaitUntil(() => Enemy.count <= 0);

        // Spawn Boss
    }

    void SpawnEnemy() {
        // Generate a random position within the spawn distance to the player
        var random_x = Random.Range(player.position.x - spawnDistance, player.position.x + spawnDistance);
        var random_y = Random.Range(player.position.y - spawnDistance, player.position.y + spawnDistance);
        var spawnPosition = new Vector3(random_x, random_y);
        
        // Move the spawn position out of the spawn distance;
        var direction = spawnPosition - player.position;
        spawnPosition += direction * spawnDistance;

        var randomIndex = Random.Range(0, 3);
        Instantiate(enemies[randomIndex], spawnPosition, Quaternion.identity);
    }
}