using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowMove : MonoBehaviour
{
    private bool facingRight;
    private CoordinateSquare square;
    private Vector2 targetPosition;
    private NavMeshAgent agent;
    private Animator anim;
    
    [SerializeField] public static float minCooldownTime = 10;
    [SerializeField] public static float maxCooldownTime = 20;
    private float lastMove = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        facingRight = true;
        // PointDirection(Random.Range(0, 2) == 0);

        lastMove = Random.Range(0, maxCooldownTime - minCooldownTime);

        anim = GetComponent<Animator>();
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        lastMove += Time.fixedDeltaTime;
        if(lastMove > maxCooldownTime)
        {
            chooseNewPosition();
            lastMove = minCooldownTime + Random.Range(0, maxCooldownTime - minCooldownTime);

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

            anim.SetFloat("x", xpos);
            anim.SetFloat("y", ypos);
        }
       
    }

    private void PointDirection(bool right)
    {
        if (facingRight != right)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }
    
    public void SetCoordinateSquare(CoordinateSquare square)
    {
        this.square = square;
    }

    public CoordinateSquare GetCoordinateSquare()
    {
        return square;
    }

    private void chooseNewPosition()
    {
        targetPosition = square.RandomCoordinate();
        agent.SetDestination(targetPosition);
        // PointDirection(targetPosition.x > transform.position.x);
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
