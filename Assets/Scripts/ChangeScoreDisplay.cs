using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject ScoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Score: " + ScoreManager.GetComponent<ScoreManager>().GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Score: " + ScoreManager.GetComponent<ScoreManager>().GetScore();
    }
}
