using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDieManager : MonoBehaviour
{
    // A list of all the possible die positions with equal frequencies
    public static bool[][] possiblePositions;

    // The time in seconds to wait before rolling again
    [SerializeField] public float timeBetweenRolls = 15f;
    private float timeSinceLastRoll = 0f;
    private int currentNumber;
    private bool[] currentPosition;

    private bool[] displayedPos;

    [SerializeField] private GameObject flowerManager;
    [SerializeField] private GameObject cowSpawner;
    [SerializeField] private GameObject[] spots;

    [SerializeField] private bool useTestDie;



    // Start is called before the first frame update
    void Start()
    {

        if (!useTestDie)
        {
            bool[] pos_one = { false, false, false, false, true, false, false, false, false };
            bool[] pos_two_a = { true, false, false, false, false, false, false, false, true };
            bool[] pos_two_b = { false, false, true, false, false, false, true, false, false };
            bool[] pos_three_a = { true, false, false, false, true, false, false, false, true };
            bool[] pos_three_b = { false, false, true, false, true, false, true, false, false };
            bool[] pos_four = { true, false, true, false, false, false, true, false, true };
            bool[] pos_five = { true, false, true, false, true, false, true, false, true };
            bool[] pos_six_a = { true, true, true, false, false, false, true, true, true };
            bool[] pos_six_b = { true, false, true, true, false, true, true, false, true };

            possiblePositions = new bool[][] { pos_one, pos_one, pos_two_a, pos_two_b, pos_three_a, pos_three_b, pos_four, pos_four, pos_five, pos_five, pos_six_a, pos_six_b };
        }
        else
        {
            bool[] pos_one_alt = { true, false, false, false, false, false, false, false, false };
            bool[] pos_two_alt = { false, true, false, false, false, false, false, false, false };
            bool[] pos_three_alt = { false, false, true, false, false, false, false, false, false };
            bool[] pos_four_alt = { false, false, false, true, false, false, false, false, false };
            bool[] pos_five_alt = { false, false, false, false, true, false, false, false, false };
            bool[] pos_six_alt = { false, false, false, false, false, true, false, false, false };
            bool[] pos_seven_alt = { false, false, false, false, false, false, true, false, false };
            bool[] pos_eight_alt = { false, false, false, false, false, false, false, true, false };
            bool[] pos_nine_alt = { false, false, false, false, false, false, false, false, true };

            possiblePositions = new bool[][] { pos_one_alt, pos_one_alt, pos_two_alt, pos_two_alt, pos_three_alt, pos_three_alt, pos_four_alt, pos_four_alt, pos_five_alt, pos_five_alt, pos_six_alt, pos_six_alt, pos_seven_alt, pos_seven_alt, pos_eight_alt, pos_eight_alt, pos_nine_alt, pos_nine_alt };
        }
        
        // Roll();
        // CowSpawner.GetComponent<SpawnCows>().SpawnSomeCows(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastRoll += Time.deltaTime;

        if (timeSinceLastRoll > timeBetweenRolls)
        {
            timeSinceLastRoll = 0f;
            StartCoroutine(RunRollDie());
        }

    }

    // Plays the animation to roll the die and rolls it
    IEnumerator RunRollDie()
    {
        for (int i = 0; i < 4; i++)
        {
            Roll(false);
            yield return new WaitForSeconds(0.25f);
            
        }

        Roll();
        cowSpawner.GetComponent<SpawnCows>().SpawnSomeCows(this);
    }

    // Rolls the die
    // If real is true, the program will set the object's variables.
    // Otherwise, it'll make it look like it rolled without changing the values
    // That way, the program doesn't spaz out for a second as the die rolling animation is playing
    private void Roll(bool real = true)
    {
        bool[] newPos;
        do
        {
            newPos = possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Length)];
        } while (newPos.Equals(currentPosition) || newPos.Equals(displayedPos));
        
        int newNumber = 0;
        
        // Loop thru the list of dots and enable/disable them as needed
        // Also count the current number
        for(int i = 0; i < 9; i++)
        {
            spots[i].GetComponent<SpriteRenderer>().enabled = newPos[i];
            if (real && newPos[i])
            {
                newNumber++;
                flowerManager.GetComponent<FlowerSpawner>().SpawnFlowers(i + 1);
            }
        }

        displayedPos = newPos;
        if (real)
        {
            currentPosition = newPos;
            currentNumber = newNumber;
        }

        // Debug.Log("New number: " + newNumber);
    }

    public float GetTimeSinceLastRoll()
    {
        return timeSinceLastRoll;
    }

    public float GetTimeUntilNextRoll(){
        return timeBetweenRolls - timeSinceLastRoll;
    }

    public bool[] GetCurrentPosition()
    {
        return currentPosition;
    }

    public int GetCurrentNumber()
    {
        return currentNumber;
    }
}
