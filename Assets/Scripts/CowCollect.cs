using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCollect : MonoBehaviour
{
    [SerializeField] GameObject scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hi");
        if (other.gameObject.CompareTag("Player"))
        {
            scoreManager.GetComponent<ScoreManager>().AddPoints(1);
            Destroy(gameObject);
        }
    }
}
