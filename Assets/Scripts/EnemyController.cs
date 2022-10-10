using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public EnemyNavPoint[] patrolNavPoints;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 3f;

    int currentPatrolNavID = -1;
    Vector3 startingPosition;

    NavMeshAgent navMeshAgent;
    public EnemyState currentState;
    EnemyState defaultState;

    GameObject player;
    


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        // Record the start position (to return to if the enemy chases the player)
        startingPosition = gameObject.transform.position;

        // If no patrol nav points have been set in the inspector then the enemy is idle (just stands where it is)
        if (patrolNavPoints == null) {
            defaultState = EnemyState.Idle;
            currentState = EnemyState.Idle;
        } else
        {
            defaultState = EnemyState.Patrol;
            currentState = EnemyState.Patrol;
            WalkToNextPatrolNavPoint();
        }
    }


    void Update()
    {
        // If the enemy is patrolling...
        if (currentState == EnemyState.Patrol)
        {
            // Check if the enemy has reached its current destination
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        // If it has, then move on to the next patrol nav point
                        WalkToNextPatrolNavPoint();
                    }
                }
            }
        }
        // If the enemy is chasing the player...
        else if (currentState == EnemyState.Chase)
        {
            // Update destination to follow player
            navMeshAgent.destination = player.transform.position;

            // If you get within range of player - attack
            // ....


            // Temporary code
            if (navMeshAgent.remainingDistance <= 1f)
            {
                Debug.Log("The player has been caught!!");
                currentState = defaultState;
                if (defaultState == EnemyState.Idle)
                {
                    ReturnToStartPlace();
                }
                else if (defaultState == EnemyState.Patrol)
                {
                    WalkToNextPatrolNavPoint();
                }
            }

        }
    }


    void WalkToNextPatrolNavPoint()
    {
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.destination = GetNextWalkTarget();
    }


    void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        currentState = EnemyState.Chase;
        navMeshAgent.destination = player.transform.position;
    }


    void ReturnToStartPlace()
    {
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.destination = startingPosition;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        // This is for the enemy to 'see' the player

        // Check if the collider which has entered the enemy's vision belongs to the player        
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            ChasePlayer();
        }
        
    }


    Vector3 GetNextWalkTarget()
    {
        // Select the next number for the walk cycle target array
        currentPatrolNavID++;

        // If you've reached the end of the array, go back to the start
        if (currentPatrolNavID >= patrolNavPoints.Length)
        {
            currentPatrolNavID = 0;
        }

        // Select the related patrolNavPoint from the array
        EnemyNavPoint nextTarget = patrolNavPoints[currentPatrolNavID];

        // Return the position of that NavPoint.
        return nextTarget.transform.position;


    }

}



public enum EnemyState
{
    Idle,
    Patrol,
    Chase
}
