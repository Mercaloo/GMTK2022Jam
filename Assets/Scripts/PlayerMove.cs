using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float slowness = 10000;
    [SerializeField] public float outOfBoundsReturnForceMultiplier = 100;
    [SerializeField] public GameObject Ground;
    [SerializeField] public Camera worldCamera;
    [SerializeField] public bool useScreenCenter = false;
    private bool onPlatform;


    // Start is called before the first frame update
    void Start()
    {
        // Go to 0, 0
        transform.position = new Vector3(0, 0, 0);
        onPlatform = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Get relative positions
        Vector2 mouseScreenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector3 tempVector3 = worldCamera.WorldToScreenPoint(transform.position);
        Vector2 playerScreenPos = new Vector2(tempVector3.x, tempVector3.y);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        
        

        //Debug.Log("Mouse:   " + mouseScreenPos);
        //Debug.Log("Current: " + screenCenter);
        Vector2 posDifference;
        
        // Lets you choose the control style
        if (useScreenCenter)
        {
            posDifference  = mouseScreenPos - screenCenter;
        }
        else
        {
            posDifference = mouseScreenPos - playerScreenPos;
        }
        
        //Debug.Log("Diff: " + posDifference);
        
        Vector2 forceToAdd = posDifference / slowness;

        //Debug.Log("Add: " + forceToAdd);
        
        // Apply force from the mouse's movement
        GetComponent<Rigidbody2D>().AddForce(forceToAdd);

        //Debugging scripts
        Debug();      


        //Check if the player is on the platform
        onPlatform = GetComponent<Collider2D>().bounds.Intersects(Ground.GetComponent<BoxCollider2D>().bounds);

        if (!onPlatform)
        {
            getBackOnPlatform();
        }

        


    }

    private void Debug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            outOfBoundsReturnForceMultiplier++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            outOfBoundsReturnForceMultiplier--;
        }
    }

    private void getBackOnPlatform()
    {
        // Get the character's position in the world and it's position relative to the center of he world
        Vector2 characterWorldPos = transform.position;
        Vector2 worldCenter = new Vector2(0, 0);

        Vector2 dir = worldCenter - characterWorldPos;

        // Make sure you're only correcting on the needed axises - otherwise, going off the right of tile 3 will bring you towards the center, not just towards the map itself
        float radius = Ground.GetComponent<BoxCollider2D>().size.x / 2;
        if(Mathf.Abs(characterWorldPos.x) < radius)
        {
            dir.x = 0;
        }
        if (Mathf.Abs(characterWorldPos.y) < radius)
        {
            dir.y = 0;
        }



        GetComponent<Rigidbody2D>().AddForce(dir * outOfBoundsReturnForceMultiplier);
    }

}
