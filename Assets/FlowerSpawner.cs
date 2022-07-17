using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    [SerializeField] GameObject cowSpawner;
    [SerializeField] GameObject flowerEffect;

    // Update is called once per frame
    public void SpawnFlowers(int region)
    {
        Instantiate(
            flowerEffect,
            cowSpawner.GetComponent<SpawnCows>().getGrid()[region - 1].GetCenter(), 
            Quaternion.identity);
    }
}
