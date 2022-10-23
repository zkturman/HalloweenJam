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

    public AudioSource chaseSound;
    public AudioSource attackSound;
    public AudioSource stunSound;
    
    float sightDistance = 3000f;   // How far the creature's 'line of sight' raycast extends
    float attackDistance = 2.5f;   // How close the creature needs to be to the player to trigger its attack.
    float attackRecoveryTime = 2f;   // How many seconds between an attack and returning to the chase
    float surveillanceTime = 3f;   // How long the creature stays in surveillance mode before returning to its default state
    float stunnedTime = 4f;     // (Perhaps this should be passed through from player when they stun the creature....?)

    int currentPatrolNavID = -1;
    Vector3 startingPosition;

    NavMeshAgent navMeshAgent;
    public CreatureState currentState;
    CreatureState defaultState;

    GameObject player;
    HealthHandler playerHealthHandler;
    private Animator monsterAnimator;
    


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponentInChildren<Animator>();
        // Record the start position (to return to if the enemy chases the player)
        startingPosition = gameObject.transform.position;

        // If no patrol nav points have been set in the inspector then the creature is idle (just stands where it is)
        if (patrolNavPoints == null || patrolNavPoints.Length < 1)
        {
            defaultState = CreatureState.Idle;
            currentState = CreatureState.Idle;
            patrolNavPoints = new PatrolNavPoint[1];
            GameObject defaultNavPoint = Instantiate(gameObject, transform.position, Quaternion.identity);
            defaultNavPoint.AddComponent<PatrolNavPoint>();
            patrolNavPoints[0] = defaultNavPoint.GetComponent<PatrolNavPoint>();
            StartCoroutine(EnterSurveillanceState());
        }
        else if (patrolNavPoints.Length == 1)
        {
            defaultState = CreatureState.Idle;
            currentState = CreatureState.Idle;
            StartCoroutine(EnterSurveillanceState());
        }
        else
        {
            defaultState = CreatureState.Patrol;
            currentState = CreatureState.Patrol;
            EnterPatrolMode();
        }
    }


    void Update()
    {
        switch (currentState)
        {
            case CreatureState.Idle:
                {
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

    void EnterIdleMode()
    {
        currentState = CreatureState.Idle;
        navMeshAgent.isStopped = true;
        monsterAnimator.SetTrigger("Idle");
    }

    void EnterPatrolMode()
    {
        // Update state
        currentState = CreatureState.Patrol;

        // Make sure movement is enabled
        navMeshAgent.isStopped = false;
        monsterAnimator.SetTrigger("Walk");

        // Set movement speed
        navMeshAgent.speed = patrolSpeed;

        // Set the next patrol destination
        SetNextPatrolDestination();
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
        // Only play chase sound effect if the creature isn't already in alert mode
        if (currentState == CreatureState.Idle ||
            currentState == CreatureState.Patrol ||
            currentState == CreatureState.Returning)
        {
            chaseSound.Play();
        }
        
        
        // Update state
        currentState = CreatureState.Chase;

        // Make sure movement is enabled
        navMeshAgent.isStopped = false;
        monsterAnimator.SetTrigger("Run");

        // Set movement speed
        navMeshAgent.speed = chaseSpeed;

        // Set destination to player's current location
        navMeshAgent.destination = player.transform.position;
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
        Ray ray = new Ray(creatureLookPosition, rayDirection);
        
        distanceToPlayer = rayDirection.magnitude;


        if (Physics.Raycast(ray, out hit, sightDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        //if (Physics.Raycast(ray, out hit, 1, QueryTriggerInteraction.Ignore))
        //if (Physics.Raycast(creatureLookPosition, rayDirection, out hit, sightDistance))
        {
            // Done as an if statement because this code will only run if the raycast hits something
            if (hit.transform.gameObject.layer == 8)    // (8 is the number of the Character layer)
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
        monsterAnimator.SetTrigger("Attack");
        attackSound.Play();
        yield return new WaitForSeconds(1f);
        Vector3 translatedPosition = transform.position - player.transform.position;
        float distanceFromPlayer = translatedPosition.magnitude;
        if (distanceFromPlayer < attackDistance)
        {
            // If this is the first attack on the player, get reference to the player's HealthHandler component
            if (playerHealthHandler == null)
            {
                Debug.Log("Acquiring reference to player HealthHandler");
                playerHealthHandler = player.GetComponentInParent<HealthHandler>();
            }
            // Damage the player
            if (currentState == CreatureState.Attack)
            {
                playerHealthHandler.TakeHealthDamage();
            }
        }

        // Allow time for animation/attack
        yield return new WaitForSeconds(attackRecoveryTime);

        // Then return to chase mode (which will test if the player is still close enough to attack again)
        EnterChaseMode();
    }


    IEnumerator EnterSurveillanceState()
    {
        currentState = CreatureState.Surveillance;
        monsterAnimator.SetTrigger("Idle");
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
        navMeshAgent.isStopped = false;
        currentPatrolNavID = -1;  // (So that when it gets to the start point it will increment it to 0 and restart its patrol loop from the beginning
        monsterAnimator.SetTrigger("Walk");
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
                EnterIdleMode();
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
            if (other.gameObject.layer == 8)   // (8 is the number of the Character layer)
            {
                Debug.Log("Creature has seen player through TRIGGER COLLIDER");
                player = other.gameObject;
                EnterChaseMode();
            }
        }    
    }

    public void StunCreature()
    {
        // This is called from outside by the player when they use the holy light on the creature

        // The stunned state is a coroutine, which could be awkward to call from other classes (?) so this public method
        // just does it internally...
        StartCoroutine(EnterStunnedMode());
    }
    

    IEnumerator EnterStunnedMode()
    {
        // Freeze creature.
        navMeshAgent.isStopped = true;
        currentState = CreatureState.Stunned;
        // *** Any other 'stunned' AV effects to be put here
        monsterAnimator.SetTrigger("Stun");
        stunSound.Play();
        BoxCollider[] allColliders = GetComponents<BoxCollider>();
        foreach(BoxCollider collider in allColliders)
        {
            collider.enabled = false;
        }
        // Wait in stunned state
        yield return new WaitForSeconds(stunnedTime);
        foreach(BoxCollider collider in allColliders)
        {
            collider.enabled = true;
        }
        // When un-stunned, go to Surveillance state to see if the player is still visible
        yield return EnterSurveillanceState();
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
