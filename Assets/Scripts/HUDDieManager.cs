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

    [SerializeField] private GameObject[] spots;


    // Start is called before the first frame update
    void Start()
    {
        bool[] pos_one     = { false, false, false, false, true,  false, false, false, false };
        bool[] pos_two_a   = { true,  false, false, false, false, false, false, false, true  };
        bool[] pos_two_b   = { false, false, true,  false, false, false, true,  false, false };
        bool[] pos_three_a = { true,  false, false, false, true,  false, false, false, true  };
        bool[] pos_three_b = { false, false, true,  false, true,  false, true,  false, false };
        bool[] pos_four    = { true,  false, true,  false, false, false, true,  false, true  };
        bool[] pos_five    = { true,  false, true,  false, true,  false, true,  false, true  };
        bool[] pos_six_a   = { true,  true,  true,  false, false, false, true,  true,  true  };
        bool[] pos_six_b   = { true, false,  true,  true,  false,  true, true,  false, true  };

        possiblePositions = new bool[][] { pos_one, pos_one, pos_two_a, pos_two_b, pos_three_a, pos_three_b, pos_four, pos_four, pos_five, pos_five, pos_six_a, pos_six_b };

        Roll();
        
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
            if (newPos[i])
            {
                newNumber++;
            }
        }

        displayedPos = newPos;
        if (real)
        {
            currentPosition = newPos;
            currentNumber = newNumber;
        }

        Debug.Log("New number: " + newNumber);
    }

    public float getTimeSinceLastRoll()
    {
        return timeSinceLastRoll;
    }

    public float getTimeUntilNextRoll(){
        return timeBetweenRolls - timeSinceLastRoll;
    }

    public bool[] getCurrentPosition()
    {
        return currentPosition;
    }

    public int getCurrentNumber()
    {
        return currentNumber;
    }
}
