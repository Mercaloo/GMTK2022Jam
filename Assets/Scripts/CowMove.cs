using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour
{
    private bool facingRight;

    
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        if (Random.Range(0, 2) == 0)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
