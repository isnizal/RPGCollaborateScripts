using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public Transform[] spawnPoints;
    public Transform slimeSpawnP, goblinSpawnP, arachnaSpawnP, mushroomSpawnP, slime2SpawnP, rockSpawnP, skeletonSpawnP, skullSpawnP;
    public Transform enemyHolder;
    public GameObject[] enemyPrefab;
    private GameObject newEnemyClone;
    public float timeBetweenSpawn, spawnDelay;
    public int slimeCounter, maxSlimeCounter, goblinCounter, maxGoblinCounter;
    public int arachnaCounter, maxArachnaCounter, mushroomCounter, maxMushroomCounter;
    public int slime2Counter, maxSlime2Counter, rockCounter, maxRockCounter;
    public int skeletonCounter, maxSkeletonCounter, skullCounter, maxSkullCounter;

	void Start()
    {
        //InvokeRepeating(nameof(EnemySpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(SlimeSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(GoblinSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(Slime2Spawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(MushroomSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(SkeletonSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(SkullSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(RockSpawn), timeBetweenSpawn, spawnDelay);
        InvokeRepeating(nameof(ArachnaSpawn), timeBetweenSpawn, spawnDelay);
    }

    void SlimeSpawn()
	{
        if (slimeCounter == maxSlimeCounter)
            return;
        if(slimeCounter < maxSlimeCounter)
		{
            newEnemyClone = Instantiate(enemyPrefab[0], slimeSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            slimeCounter++;
		}
	}
    void GoblinSpawn()
    {
        if (goblinCounter == maxGoblinCounter)
            return;
        if (goblinCounter < maxGoblinCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[1], goblinSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            goblinCounter++;
        }
    }
    void Slime2Spawn()
    {
        if (slime2Counter == maxSlime2Counter)
            return;
        if (slime2Counter < maxSlime2Counter)
        {
            newEnemyClone = Instantiate(enemyPrefab[2], slime2SpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            slime2Counter++;
        }
    }
    void MushroomSpawn()
    {
        if (mushroomCounter == maxMushroomCounter)
            return;
        if (mushroomCounter < maxMushroomCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[3], mushroomSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            mushroomCounter++;
        }
    }
    void SkeletonSpawn()
    {
        if (skeletonCounter == maxSkeletonCounter)
            return;
        if (skeletonCounter < maxSkeletonCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[4], skeletonSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            skeletonCounter++;
        }
    }
    void SkullSpawn()
    {
        if (skullCounter == maxSkeletonCounter)
            return;
        if (skullCounter < maxSkullCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[5], skullSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            skullCounter++;
        }
    }
    void RockSpawn()
    {
        if (rockCounter == maxRockCounter)
            return;
        if (rockCounter < maxRockCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[6], rockSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            rockCounter++;
        }
    }
    void ArachnaSpawn()
    {
        if (arachnaCounter == maxArachnaCounter)
            return;
        if (arachnaCounter < maxArachnaCounter)
        {
            newEnemyClone = Instantiate(enemyPrefab[7], arachnaSpawnP.position, Quaternion.identity) as GameObject;
            newEnemyClone.transform.parent = enemyHolder;
            arachnaCounter++;
        }
    }


}
