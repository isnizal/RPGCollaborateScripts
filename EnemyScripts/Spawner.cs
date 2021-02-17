using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform enemyHolder;
    public GameObject[] enemyPrefab;
    public float timeBetweenSpawn, spawnDelay;
    public int spawnCounter, maxSpawns;

    void Start()
    {
        InvokeRepeating("EnemySpawn", timeBetweenSpawn, spawnDelay);
    }

    void EnemySpawn()
	{
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        if (spawnCounter == maxSpawns)
            return;
        if(spawnCounter < maxSpawns)
		{
            GameObject newClone = Instantiate(enemyPrefab[0], spawnPoints[randomSpawnPoint].position, Quaternion.identity) as GameObject;
            newClone.transform.parent = enemyHolder;
            spawnCounter++;
		}
	}
}
