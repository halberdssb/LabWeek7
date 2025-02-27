using UnityEngine;
using UnityEngine.AI;
public class NavMeshEnemy : MonoBehaviour
{
    public Transform patrolRoute; // Parent containing waypoints
    public Transform player; // Player reference
    public Material normalMaterial;
    public Material alertedMaterial;
    private NavMeshAgent agent;
    public Transform[] locations;
    private int currentLocation = 0;
    private bool chasingPlayer = false;
    private MeshRenderer meshRenderer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }
    void Update()
    {
        if (!chasingPlayer && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation();
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (locations.Length == 0) return;
        agent.SetDestination(locations[currentLocation].position);
        currentLocation = (currentLocation + 1) % locations.Length;
    }
    void InitializePatrolRoute()
    {
        locations = new Transform[patrolRoute.childCount];
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i);
        }
    }
    // Detect when player enters enemy's range
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {

            Debug.Log("Player detected - start chasing!");
            meshRenderer.material = alertedMaterial;
            chasingPlayer = true;
            agent.SetDestination(player.position);
        }
    }
    // Detect when player leaves enemy's range
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range - resume patrol.");
            meshRenderer.material = normalMaterial;
            chasingPlayer = false;
            MoveToNextPatrolLocation();
        }
    }
}
