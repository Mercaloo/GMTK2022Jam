using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // Go to 0, 0
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        Debug.Log("Mouse:   " + mousePos);
        Debug.Log("Current: " + currentPos);
        
        
        Vector2 posDifference = mousePos - currentPos;

        Vector2 forceToAdd = posDifference / 1000;


        GetComponent<Rigidbody2D>().AddForce(forceToAdd);
    }
}
