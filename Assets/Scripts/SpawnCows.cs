using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCows : MonoBehaviour
{
    [SerializeField] GameObject cow;
    [SerializeField] int minCows = 6;
    [SerializeField] int maxCows = 20;
    public List<GameObject> farmers = new List<GameObject>();


    [SerializeField] int ZONE_WIDTH = 12;
    [SerializeField] int BORDER_WIDTH = 1;
    
    CoordinateSquare[] squares;

    private void Start()
    {
        CoordinateSquare square1 = new CoordinateSquare(-ZONE_WIDTH * 3 / 2 - BORDER_WIDTH, -ZONE_WIDTH     / 2 - BORDER_WIDTH,  ZONE_WIDTH * 3 / 2 + BORDER_WIDTH,  ZONE_WIDTH     / 2 + BORDER_WIDTH, 1);
        CoordinateSquare square2 = new CoordinateSquare(-ZONE_WIDTH     / 2,  ZONE_WIDTH     / 2,  ZONE_WIDTH * 3 / 2 + BORDER_WIDTH,  ZONE_WIDTH     / 2 + BORDER_WIDTH, 2);
        CoordinateSquare square3 = new CoordinateSquare( ZONE_WIDTH     / 2 + BORDER_WIDTH,  ZONE_WIDTH * 3 / 2 + BORDER_WIDTH,  ZONE_WIDTH * 3 / 2 + BORDER_WIDTH,  ZONE_WIDTH     / 2 + BORDER_WIDTH, 3);
        CoordinateSquare square4 = new CoordinateSquare(-ZONE_WIDTH * 3 / 2 - BORDER_WIDTH, -ZONE_WIDTH     / 2 - BORDER_WIDTH,  ZONE_WIDTH / 2,     -ZONE_WIDTH     / 2, 4);
        CoordinateSquare square5 = new CoordinateSquare(-ZONE_WIDTH / 2,      ZONE_WIDTH     / 2,      ZONE_WIDTH     / 2, -ZONE_WIDTH / 2, 5);
        CoordinateSquare square6 = new CoordinateSquare( ZONE_WIDTH / 2 + BORDER_WIDTH,      ZONE_WIDTH * 3 / 2 + BORDER_WIDTH,  ZONE_WIDTH / 2,     -ZONE_WIDTH     / 2, 6);
        CoordinateSquare square7 = new CoordinateSquare(-ZONE_WIDTH * 3 / 2 - BORDER_WIDTH, -ZONE_WIDTH     / 2 - BORDER_WIDTH, -ZONE_WIDTH / 2 - BORDER_WIDTH,     -ZONE_WIDTH * 3 / 2 - BORDER_WIDTH, 7);
        CoordinateSquare square8 = new CoordinateSquare(-ZONE_WIDTH / 2,      ZONE_WIDTH     / 2, -ZONE_WIDTH / 2 - BORDER_WIDTH,     -ZONE_WIDTH * 3 / 2 - BORDER_WIDTH, 8);
        CoordinateSquare square9 = new CoordinateSquare( ZONE_WIDTH / 2 + BORDER_WIDTH,      ZONE_WIDTH * 3 / 2 + BORDER_WIDTH, -ZONE_WIDTH / 2 - BORDER_WIDTH,     -ZONE_WIDTH * 3 / 2 - 1, 9);

        squares = new CoordinateSquare[] { square1, square2, square3, square4, square5, square6, square7, square8, square9};

        Debug.Log("Moo?");
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
