using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerTakePicture : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Get the object with the GameController tag
            GameObject gameController = GameObject.FindWithTag("GameController");
            gameController.GetComponent<GameStatusManager>().GameOver();
            
        }
    }
}
