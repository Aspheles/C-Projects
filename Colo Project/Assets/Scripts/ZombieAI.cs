using UnityEngine;
using System.Collections;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.vThirdPersonAnimator;
//using UnityEngine.Transform;
//using UnityEngine.Vector3;
//using static UnityEngine.Component;
//using static UnityEngine.Animator;

public class ZombieAI : MonoBehaviour
{
    public enum WanderType { Random, Waypoint }
    

    public vThirdPersonCamera fpsc;
    public WanderType wanderType = WanderType.Random;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public Transform[] waypoints;

    public Transform player;
    float distancefrom_player;
    float gainingDistance = 10f;
    public float hitRange = 1.5f;

    private bool isAware = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int waypointIndex = 0;
    private Animator animator;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
    }

    public void Update()
    {
        /*Calculating distance between zombie and player*/
        distancefrom_player = Vector3.Distance(player.position, transform.position);

        /*If the zombie spots the player while wandering then he would start chasing you with the initial set chase speed*/
        if (isAware)
        {
            agent.SetDestination(fpsc.transform.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;
            renderer.material.color = Color.red;
            Debug.Log("I can see you");
        }

        /*If the zombie didn't spot you yet then he is just wandering around searching for the player*/
        else
        {
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
            renderer.material.color = Color.blue;
            Debug.Log("I can't see you");
        }

        /*If zombie comes close to the player the "hit" animation is played and zombie chase speed becomes 0*/
        if (distancefrom_player < hitRange)
        {
            animator.SetBool("Punch", true);
            chaseSpeed = 0;
            transform.LookAt(player);
        }

        /*If the player start running again the "hit" animation is stopped and zombie goes back to chasing and chase speed becomes 3.5f*/
        else
        {
            animator.SetBool("Punch", false);
            chaseSpeed = 3.5f;
            //animator.play("Run");
        }

        /*If the player gains xx amount of distance from the zombie then the zombie should go back to "wandering" state*/
        if (gainingDistance < distancefrom_player)
        {
            isAware = false;
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
        }
    }
	
    //Spotting the player
    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, fpsc.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }
    public void OnAware()
    {
        isAware = true;
    }

    //Random Wandering
    public void Wander()
    {
        if (wanderType == WanderType.Random)//3.5 tut
        {

        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
        }
        else
        {   //Waypoint Wandering
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f)
                {
                    if (waypointIndex == waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].position);
                }
            }
            else
            {
                Debug.LogWarning("More than 1 waypoints needs to be assigned: " + gameObject.name);
            }
        }
    } 

    //Random wandering method
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}

