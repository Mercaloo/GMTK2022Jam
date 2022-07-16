using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCows : MonoBehaviour
{
    [SerializeField] GameObject cow;
    [SerializeField] int minCows = 6;
    [SerializeField] int maxCows = 20;
    public List<GameObject> farmers = new List<GameObject>();


    int zoneWidth = 6;
    
    CoordinateSquare[] squares;

    private void Start()
    {
        CoordinateSquare square1 = new CoordinateSquare(-zoneWidth * 3 / 2 - 1, -zoneWidth     / 2 - 1,  zoneWidth * 3 / 2 + 1,  zoneWidth     / 2 + 1, 1);
        CoordinateSquare square2 = new CoordinateSquare(-zoneWidth     / 2,  zoneWidth     / 2,  zoneWidth * 3 / 2 + 1,  zoneWidth     / 2 + 1, 2);
        CoordinateSquare square3 = new CoordinateSquare( zoneWidth     / 2 + 1,  zoneWidth * 3 / 2 + 1,  zoneWidth * 3 / 2 + 1,  zoneWidth     / 2 + 1, 3);
        CoordinateSquare square4 = new CoordinateSquare(-zoneWidth * 3 / 2 - 1, -zoneWidth     / 2 - 1,  zoneWidth / 2,     -zoneWidth     / 2, 4);
        CoordinateSquare square5 = new CoordinateSquare(-zoneWidth / 2,      zoneWidth     / 2,      zoneWidth     / 2, -zoneWidth / 2, 5);
        CoordinateSquare square6 = new CoordinateSquare( zoneWidth / 2 + 1,      zoneWidth * 3 / 2 + 1,  zoneWidth / 2,     -zoneWidth     / 2, 6);
        CoordinateSquare square7 = new CoordinateSquare(-zoneWidth * 3 / 2 - 1, -zoneWidth     / 2 - 1, -zoneWidth / 2 - 1,     -zoneWidth * 3 / 2 - 1, 7);
        CoordinateSquare square8 = new CoordinateSquare(-zoneWidth / 2,      zoneWidth     / 2, -zoneWidth / 2 - 1,     -zoneWidth * 3 / 2 - 1, 8);
        CoordinateSquare square9 = new CoordinateSquare( zoneWidth / 2 + 1,      zoneWidth * 3 / 2 + 1, -zoneWidth / 2 - 1,     -zoneWidth * 3 / 2 - 1, 9);

        squares = new CoordinateSquare[] { square1, square2, square3, square4, square5, square6, square7, square8, square9};

        Debug.Log(farmers);
    }



    // Spawns cows in the regions indicated by the die
    // Makes sure to spawn at least one in each region indicated on the die, then spawns the rest randomly
    public void SpawnSomeCows(HUDDieManager dieManager)
    {

        // Debug.Log("Moo.");

        int cowsLeftToSpawn = Random.Range(minCows, maxCows + 1);
        
        // Spawn one cow in every indicated region
        for(int i = 0; i < 9; i++)
        {
            if (dieManager.GetCurrentPosition()[i])
            {
                cowsLeftToSpawn--;
                SpawnCowInRegion(i + 1);
            }
        }

        // Spawn the rest of the cows randomly
        for(int i = 0; i < cowsLeftToSpawn; i++)
        {
            int regionToSpawn;
            do
            {
                regionToSpawn = Random.Range(1, 10);
            } while (!dieManager.GetCurrentPosition()[regionToSpawn - 1]);

            SpawnCowInRegion(regionToSpawn);

        }

        // Make the farmers start targeting cows
        foreach (GameObject farmer in farmers)
        {
            farmer.GetComponent<FarmerMove>().ChooseNewTargetCow();
        }

    }


    void SpawnCowInRegion(int region)
    {
        GameObject newCow = Instantiate(cow, squares[region - 1].RandomCoordinate(), Quaternion.identity);
        newCow.GetComponent<CowMove>().SetCoordinateSquare(squares[region - 1]);
    }

    public GameObject[] GetListOfCows() {
        return GameObject.FindGameObjectsWithTag("Cow");
    }
}
