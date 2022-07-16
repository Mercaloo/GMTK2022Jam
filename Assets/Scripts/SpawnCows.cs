using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCows : MonoBehaviour
{
    [SerializeField] int minCows = 6;
    [SerializeField] int maxCows = 20;
    int zoneWidth = 6;
    CoordinateSquare[] squares;
    [SerializeField] GameObject cow;

    private class CoordinateSquare
    {

        public int minX, minY, maxX, maxY;
        public CoordinateSquare(int minX, int maxX, int minY, int maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public Vector2 RandomCoordinate()
        {
            return new Vector2(Random.Range(minX * 1f, maxX * 1f), Random.Range(minY * 1f, maxY * 1f));
        }
    }

    private void Start()
    {
        

        CoordinateSquare square1 = new CoordinateSquare(-zoneWidth * 3 / 2, -zoneWidth     / 2,  zoneWidth * 3 / 2,  zoneWidth     / 2);
        CoordinateSquare square2 = new CoordinateSquare(-zoneWidth     / 2,  zoneWidth     / 2,  zoneWidth * 3 / 2,  zoneWidth     / 2);
        CoordinateSquare square3 = new CoordinateSquare( zoneWidth     / 2,  zoneWidth * 3 / 2,  zoneWidth * 3 / 2,  zoneWidth     / 2);
        CoordinateSquare square4 = new CoordinateSquare(-zoneWidth * 3 / 2, -zoneWidth     / 2,  zoneWidth / 2,     -zoneWidth     / 2);
        CoordinateSquare square5 = new CoordinateSquare(-zoneWidth / 2,      zoneWidth     / 2, -zoneWidth / 2,      zoneWidth     / 2);
        CoordinateSquare square6 = new CoordinateSquare( zoneWidth / 2,      zoneWidth * 3 / 2,  zoneWidth / 2,     -zoneWidth     / 2);
        CoordinateSquare square7 = new CoordinateSquare(-zoneWidth * 3 / 2, -zoneWidth     / 2, -zoneWidth / 2,     -zoneWidth * 3 / 2);
        CoordinateSquare square8 = new CoordinateSquare(-zoneWidth / 2,      zoneWidth     / 2, -zoneWidth / 2,     -zoneWidth * 3 / 2);
        CoordinateSquare square9 = new CoordinateSquare( zoneWidth / 2,      zoneWidth * 3 / 2, -zoneWidth / 2,     -zoneWidth * 3 / 2);

        squares = new CoordinateSquare[] { square1, square2, square3, square4, square5, square6, square7, square8, square9};


    }



    // Spawns cows in the regions indicated by the die
    // Makes sure to spawn at least one in each region indicated on the die, then spawns the rest randomly
    public void SpawnSomeCows(HUDDieManager dieManager)
    {

        Debug.Log("Moo.");

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
    }


    void SpawnCowInRegion(int region)
    {
        GameObject newCow = Instantiate(cow, squares[region - 1].RandomCoordinate(), Quaternion.identity);
        newCow.GetComponent<CowMove>().SetRegion(region);
    }

    public GameObject[] GetListOfCows() {
        return GameObject.FindGameObjectsWithTag("Cow");
    }
}
