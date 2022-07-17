using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private float enemySpawnCooldown = 30f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject cowSpawner;
    [SerializeField] private GameObject dieManager;
    private int extraEnemiesSpawned = 0;
    
    private float spawnTimer;
    [SerializeField] private Vector2 enemySpawnLocation = new Vector2(7, 0);

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = enemySpawnCooldown - 8;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        // Debug.Log(spawnTimer);
        if (spawnTimer > enemySpawnCooldown)
        {
            spawnTimer = 0;
            extraEnemiesSpawned += 1;
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnLocation, Quaternion.identity);
            enemy.GetComponent<FarmerMove>().setCowSpawner(cowSpawner);
            enemy.GetComponent<FarmerMove>().setDieManager(dieManager);
            enemy.GetComponent<NavMeshAgent>().speed += Random.Range(-extraEnemiesSpawned * 1f, extraEnemiesSpawned * 1f);
        }
    }
}
