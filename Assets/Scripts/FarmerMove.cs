using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerMove : MonoBehaviour
{
    [SerializeField] GameObject cowSpawner;
    [SerializeField] private GameObject dieManagerObject;
    private HUDDieManager dieManager;
    private GameObject currentCow;
    private Vector2 targetPosition;
    private NavMeshAgent agent;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        dieManager = dieManagerObject.GetComponent<HUDDieManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ChooseNewTargetCow();
        cowSpawner.GetComponent<SpawnCows>().farmers.Add(gameObject);

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance < 0.5f)
        {
            ChooseNewTargetCow();
        }

        float xpos = GetXSpeed();
        float ypos = GetYSpeed();

        if(xpos == 0 && ypos == 0){
            anim.SetBool("Walk", false);
        }

        if(xpos != 0 && ypos != 0){
            anim.SetBool("Walk", true);
        }


        if(anim.GetBool("Walk")){

            anim.SetFloat("x", Mathf.Round(xpos));
            anim.SetFloat("y", Mathf.Round(ypos));
        }

    }

    public void ChooseNewTargetCow()
    {
        // Choose a random cow in the proper zone
        GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
        GameObject testCow;
        List<GameObject> choosableCows = new List<GameObject>();
        for (int i = 0; i < cows.Length; i++)
        {
            testCow = cows[i];
            
            // See if the cow is in a correct zone
            
            if(dieManager.
                GetCurrentPosition()[
                testCow.
                GetComponent<CowMove>().
                GetCoordinateSquare().
                getRegionNumber() - 1]
                
                && !testCow.
                Equals(
                    currentCow))
            {
                choosableCows.Add(testCow);
            }
        }

        if(choosableCows.Count == 0)
        {
            // Debug.Log("No cows in the correct zone");
            // TODO: Do question mark animation and stand around
            currentCow = null;
        }
        else
        {
            // Choose a random cow from the list
            int randomCow = Random.Range(0, choosableCows.Count);
            currentCow = choosableCows[randomCow];
            targetPosition = currentCow.transform.position;
            agent.SetDestination(targetPosition);
            // Debug.Log("New cow!");
        }

    }

    public void setCowSpawner(GameObject spawner)
    {
        cowSpawner = spawner;
    }

    public void setDieManager(GameObject dieManager)
    {
        dieManagerObject = dieManager;
    }

    
    public float GetXSpeed()
    {
        Debug.Log(agent.velocity.x);
        return agent.velocity.x;
        
    }

    public float GetYSpeed()
    {
        Debug.Log(agent.velocity.y);
        return agent.velocity.y;
    }
}
