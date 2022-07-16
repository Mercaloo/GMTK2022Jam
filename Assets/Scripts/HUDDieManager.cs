using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDieManager : MonoBehaviour
{
    // A list of all the possible die positions with equal frequencies
    public static bool[][] positions;

    // The time in seconds to wait before rolling again
    [SerializeField] public float timeBetweenRolls = 15f;
    private float timeSinceLastRoll = 0f;
    private int currentNumber;
    private bool[] currentPosition;

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

        positions = new bool[][] { pos_one, pos_one, pos_two_a, pos_two_b, pos_three_a, pos_three_b, pos_four, pos_four, pos_five, pos_five, pos_six_a, pos_six_b };

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

    IEnumerator RunRollDie()
    {
        for (int i = 0; i < 4; i++)
        {
            Roll();
            yield return new WaitForSeconds(0.25f);
            
        }

        Roll();
    }

    
    private void Roll()
    {
        bool[] newPos;
        do
        {
            newPos = positions[UnityEngine.Random.Range(0, positions.Length)];
        } while (newPos.Equals(currentPosition));

        currentPosition = newPos;
        currentNumber = 0;
        
        // Loop thru the list of dots and enable/disable them as needed
        // Also count the current number
        for(int i = 0; i < 9; i++)
        {
            spots[i].GetComponent<SpriteRenderer>().enabled = currentPosition[i];
            if (currentPosition[i])
            {
                currentNumber++;
            }
        }

        Debug.Log("New number: " + currentNumber);
    }

    public float getTimeSinceLastRoll()
    {
        return timeSinceLastRoll;
    }

    public float getTimeUntilNextRoll(){
        return timeBetweenRolls - timeSinceLastRoll;
    }
}
