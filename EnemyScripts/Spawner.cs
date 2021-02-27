using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform enemyHolder;
    public GameObject[] enemyPrefab;
    public GameObject newEnemyClone;
    public float timeBetweenSpawn, spawnDelay;
    public int spawnCounter, maxSpawns;

    void Update()
    {
        InvokeRepeating(nameof(EnemySpawn), timeBetweenSpawn, spawnDelay);
    }

    void EnemySpawn()
	{
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        if (spawnCounter == maxSpawns)
            return;
        if(spawnCounter < maxSpawns)
		{
            newEnemyClone = Instantiate(enemyPrefab[0], spawnPoints[randomSpawnPoint].position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            spawnCounter++;
		}
	}
}
