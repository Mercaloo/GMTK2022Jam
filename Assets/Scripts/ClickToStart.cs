using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    private bool showingTutorial = false;
    [SerializeField] GameObject tutorial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!showingTutorial)
            {
                tutorial.GetComponent<SpriteRenderer>().enabled = true;
                showingTutorial = true;
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
