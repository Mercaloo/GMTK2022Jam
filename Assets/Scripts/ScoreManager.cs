using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int currentScore;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    public void AddPoints(int score)
    {
        currentScore += score;
        Debug.Log("Current score: " + currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

}
