using UnityEngine;
using UnityEngine.AI;

//This script handles zombie movement using NavMesh
public class ZombieAI : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent agent;
    public float stoppingDistance; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        // Continuously set the destination to the player's position
        agent.SetDestination(playerTransform.position); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // When the zombie's collider triggers with the player, stop the NavMeshAgent from moving
            agent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player exits the trigger, allow the NavMeshAgent to move again
            agent.isStopped = false;
        }
    }
}
