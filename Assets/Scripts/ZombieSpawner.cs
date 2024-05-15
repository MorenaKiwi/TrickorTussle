using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform playerTransform;
    private float spawnRange = 20;
    private float startDelay = 4;
    private float spawnInterval = 1.5f;
    

    void Start()
    {
        InvokeRepeating("SpawnZombie", startDelay, spawnInterval);
    }

    void SpawnZombie()
    {
        
        float randomAngle = Random.Range(0, 360);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        float spawnPosX = playerTransform.position.x + spawnRange * Mathf.Cos(angleInRadians);
        float spawnPosZ = playerTransform.position.z + spawnRange * Mathf.Sin(angleInRadians);

        Vector3 spawnPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemy = zombie.GetComponent<Enemy>();

        
        enemy.target = playerTransform;
        
    }
}
