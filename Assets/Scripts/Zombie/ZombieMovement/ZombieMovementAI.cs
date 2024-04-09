using UnityEngine;
using UnityEngine.AI;

//This script manages the movement of zombie objects towards the player
public class ZombieAI : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Ensure your player has the "Player" tag
    }

    void Update()
    {
        agent.SetDestination(playerTransform.position); 
    }
}
