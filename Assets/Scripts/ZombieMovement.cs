using UnityEngine;
using UnityEngine.AI; // OBLIGATORIU pentru NavMeshAgent

public class ZombieMovement : MonoBehaviour
{
    [Header("Referinte")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Setari Navigatie")]
    public float range = 10f;
    public float stopDist = 1.1f; 

    [Header("Setari Atac")]
    public int attackDamage = 10;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.stoppingDistance = stopDist;
    }
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < range)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            // Logic check: Are we close enough AND is the timer ready?
            // We check Time.time HERE before even calling the function
            if (dist <= (agent.stoppingDistance + 0.3f) && Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
        else
        {
            agent.isStopped = true;
        }

        RotateSprite();
    }

    void Attack()
    {
        // 1. Reset timer IMMEDIATELY
        nextAttackTime = Time.time + attackRate;

        // 2. Try to damage the player
        PlayerController playerHealth = player.GetComponent<PlayerController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            // This will tell us which specific zombie is the killer
            Debug.Log($"{gameObject.name} bit you! Next attack at: {nextAttackTime}");
        }
    }


    void RotateSprite()
    {
       
        if (agent.velocity.x > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (agent.velocity.x < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}