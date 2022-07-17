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

        lastMove = Random.Range(minCooldownTime, maxCooldownTime);
        
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
        return agent.velocity.x;
    }

    public float GetYSpeed()
    {
        return agent.velocity.y;
    }

}
