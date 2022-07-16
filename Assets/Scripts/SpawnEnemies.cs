using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private int[] enemySpawnTimes = { 10, 60, 120, 150, 180, 210, 240, 270, 300, 330};
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject cowSpawner;
    [SerializeField] private GameObject dieManager;

    private int lastSpawnIndex = -1;
    private float spawnTimer = 0;
    [SerializeField] private Vector2 enemySpawnLocation = new Vector2(7, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > enemySpawnTimes[lastSpawnIndex + 1])
        {
            lastSpawnIndex++;
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnLocation, Quaternion.identity);
            enemy.GetComponent<FarmerMove>().setCowSpawner(cowSpawner);
            enemy.GetComponent<FarmerMove>().setDieManager(dieManager);
        }
    }
}
