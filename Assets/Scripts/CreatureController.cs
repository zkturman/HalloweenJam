using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreatureController : MonoBehaviour
{
    public PatrolNavPoint[] patrolNavPoints;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 3f;
    float sightDistance = 3000f;   // How far the creature's 'line of sight' raycast extends
    float attackDistance = 1f;   // How close the creature needs to be to the player to trigger its attack.
    float attackRecoveryTime = 1f;   // How many seconds between an attack and returning to the chase
    float surveillanceTime = 3f;   // How long the creature stays in surveillance mode before returning to its default state

    int currentPatrolNavID = -1;
    Vector3 startingPosition;

    NavMeshAgent navMeshAgent;
    public CreatureState currentState;
    CreatureState defaultState;

    GameObject player;
    
    


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        // Record the start position (to return to if the enemy chases the player)
        startingPosition = gameObject.transform.position;

        // If no patrol nav points have been set in the inspector then the creature is idle (just stands where it is)
        if (patrolNavPoints == null) {
            defaultState = CreatureState.Idle;
            currentState = CreatureState.Idle;
        } else
        {
            defaultState = CreatureState.Patrol;
            currentState = CreatureState.Patrol;
            SetNextPatrolDestination();
        }
    }


    void Update()
    {
        switch (currentState)
        {
            case CreatureState.Idle:
                {
                    // Do nothing, currently...
                    break;
                }
            case CreatureState.Patrol:
                {
                    Patrol();
                    break;
                }
            case CreatureState.Chase:
                {
                    Chase();
                    break;
                }
            case CreatureState.Attack:
                {
                    // Do nothing here - the 'attack' action is a coroutine called when the creature first enters the 'attack' state
                    // It's not designed to be called each frame.
                    break;
                }
            case CreatureState.Surveillance:
                {
                    // Do nothing here - the 'surveillance' action is a coroutine called when the creature first enters the 'attack' state
                    // It's not designed to be called each frame.
                    break;
                }
            case CreatureState.Returning:
                {
                    Returning();
                    break;
                }
            default: break;
        }
    }


    void EnterPatrolMode()
    {
        // Update state
        currentState = CreatureState.Patrol;

        // Make sure movement is enabled
        navMeshAgent.isStopped = false;

        // Set movement speed
        navMeshAgent.speed = patrolSpeed;

        // Set the next patrol destination
        SetNextPatrolDestination();

        // ??? Animation change here???

    }

    void Patrol()
    {
        // This is called each frame while the creature is in patrol mode.
        
        // If the creature has reached its NavMesh destinaton then set a new destination
        if (HasReachedDestination())
        {
            SetNextPatrolDestination();
        }

        // Otherwise just let the navmesh continue to move it towards its current destination...
    }


    void EnterChaseMode()
    {
        // Update state
        currentState = CreatureState.Chase;

        // Make sure movement is enabled
        navMeshAgent.isStopped = false;

        // Set movement speed
        navMeshAgent.speed = chaseSpeed;

        // Set destination to player's current location
        navMeshAgent.destination = player.transform.position;

        // ??? Animation change here???
    }


    void Chase()
    {
        // This is called each frame while the creature is in chase mode

        // First raycast to check if the creature has line of sight to the player
        bool canSeePlayer = false;
        float distanceToPlayer;

        // Vector3 position of player and creature
        Vector3 playerLookPosition = player.transform.position + (Vector3.up * 1.25f);
        Vector3 creatureLookPosition = this.transform.position + (Vector3.up * 1.5f);


        RaycastHit hit;
        Vector3 rayDirection = playerLookPosition - creatureLookPosition;
        distanceToPlayer = rayDirection.magnitude;

        if (Physics.Raycast(creatureLookPosition, rayDirection, out hit, sightDistance))
        {
            // Done as an if statement because this code will only run if the raycast hits something
                //if (hit.rigidbody.gameObject.CompareTag("Player"))
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                // Creature can see player
                canSeePlayer = true;
            }
        }

        if (canSeePlayer)
        {
            // Red debug line if the creature can see the player
            Debug.DrawRay(creatureLookPosition, rayDirection, Color.red, 0f, true);


            if (distanceToPlayer <= attackDistance)
            {
                // If in range to attack then update state and attack on next frame
                StartCoroutine(Attack());
                return;
            }
            else
            {
                // Otherwise player is out of attack range, so just update destination to player's latest location (the creature will continue moving towards 
                navMeshAgent.destination = player.transform.position;

                // *** Additional code that if the player is somewhere it can't navigate to, set destination as the nearest point to the player ***

                return;
            }

        }
        else
        {

            // Can't see player, so just continues moving to the current destination (where the player was last seen)

            // Yellow debug line if the creature can see the player
            Debug.DrawRay(creatureLookPosition, rayDirection, Color.yellow, 0f, true);

            // If the creature has reached its target, enter surveillance state
            if (HasReachedDestination())
            {
                StartCoroutine(EnterSurveillanceState());
            }
        }

    }


    IEnumerator Attack()
    {
        currentState = CreatureState.Attack;
        
        // Pause movement towards the target
        navMeshAgent.isStopped = true;

        // Temporary code
        Debug.Log("ATTTAAAAACKKK!!!!");

        // Allow time for animation/attack
        yield return new WaitForSeconds(attackRecoveryTime);

        // Then return to chase mode (which will test if the player is still close enough to attack again)
        EnterChaseMode();
    }


    IEnumerator EnterSurveillanceState()
    {
        currentState = CreatureState.Surveillance;
        
        float timer = 0f;
        while (timer < surveillanceTime)
        {
            yield return null;

            // *** Detect if creature has changed state ***
            if (currentState != CreatureState.Surveillance)
            {
                // No longer in surveillance state, so end the coroutine without doing anything else
                yield break;
                Debug.Log("WARNING: Coroutine EnterSurveillanceState should have stopped, so this message shouldn't be seen!");
            }
            timer += Time.deltaTime;
        }

        // Once the surveillance timer runs out (without spotting the player), return to normal state
        ReturnToNormalState();
    }



    void SetNextPatrolDestination()
    {
        // Update the navmesh agent speed with the value in the public inspector
        // (This is for testing purposes only so the speed can be tweaked during a patrol cycle - it wouldn't be needed in the build version)
        navMeshAgent.speed = patrolSpeed;

        // Select the next number for the walk cycle target array
        currentPatrolNavID++;

        // If you've reached the end of the array, go back to the start
        if (currentPatrolNavID >= patrolNavPoints.Length)
        {
            currentPatrolNavID = 0;
        }

        // Set the NavMeshAgent destination to the position of the newly selected NavPoint
        navMeshAgent.destination = patrolNavPoints[currentPatrolNavID].transform.position;
    }


    bool HasReachedDestination()
    {
        bool answer = false;
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    answer = true;
                }
            }
        }
        
        return answer;

    }


    void ReturnToNormalState()
    {
        // After a chase, the creature returns to its normal location and default behaviour
        currentState = CreatureState.Returning;
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.destination = startingPosition;
        currentPatrolNavID = -1;  // (So that when it gets to the start point it will increment it to 0 and restart its patrol loop from the beginning
    }
    

    void Returning()
    {
        // This is called each frame while the creature is returning to its normal location

        // Once it has returned to its starting point, revert to its default behaviour
        if (HasReachedDestination())
        {
            if (defaultState == CreatureState.Patrol)
            {
                EnterPatrolMode();
            }
            else
            {
                // Assume default state is idle
                // For now just leave the creature standing still - but maybe this should be linked to surveillance type behaviour later.
                currentState = CreatureState.Idle;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        // This is called when the creature 'sees' the player

        // It is only relevant if the creature is not already in pursuit of the player
        if (currentState == CreatureState.Idle || currentState == CreatureState.Patrol || currentState == CreatureState.Surveillance || currentState == CreatureState.Returning)
        {
            // Check if the collider which has entered the creature's vision belongs to the player        
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Creature has seen player through TRIGGER COLLIDER");
                player = other.gameObject;
                EnterChaseMode();
            }
        }    
    }

    public void Stun()
    {
        // This is called from outside by the player when they use the holy light on the creature

        // Remember the current state, then freeze the 
    }
    

}



public enum CreatureState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Surveillance,
    Returning,
    Stunned
}
