using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Referinte")]
    public Transform player;
    private NavMeshAgent agent;
    private Animator anim; // Added Animator reference
    private Vector3 baseScale;
    [Header("Setari Navigatie")]
    public float range = 10f;
    public float stopDist = 1.1f;

    [Header("Setari Atac")]
    public int attackDamage = 10;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    void Start()
    {
        baseScale = transform.localScale;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Initialize Animator

        // Find player if not assigned
        if (player == null)
        {
            var pc = FindFirstObjectByType<PlayerController>();
            if (pc != null) player = pc.transform;
        }

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

            if (dist <= (agent.stoppingDistance + 0.3f) && Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
        else
        {
            agent.isStopped = true;
        }

        UpdateAnimations(); // Logic for switching sprites
    }

    void UpdateAnimations()
    {
        // 1. Get current movement velocity from the NavMeshAgent
        Vector2 velocity = agent.velocity;
       
        // 3. Determine which animation to play based on velocity
        if (velocity.magnitude < 0.1f)
        {
            // Zombie is standing still or reached destination
            anim.SetBool("idle", true);
            anim.SetBool("backWalk", false);
            anim.SetBool("walkFront", false);
            anim.SetBool("sidesWalk", false);
        }
        else
        {
            // Check if movement is primarily Vertical (Up/Down) or Horizontal (Left/Right)
            if (Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x))
            {
                // Vertical movement priority
                if (velocity.y > 0)
                {
                    anim.SetBool("backWalk", true);  // Moving Up
                    anim.SetBool("walkFront", false);
                    anim.SetBool("sidesWalk", false);
                    anim.SetBool("idle", false);
                }

                else
                {
                    anim.SetBool("walkFront", true); // Moving Down
                    anim.SetBool("sidesWalk", false);
                    anim.SetBool("idle", false);
                    anim.SetBool("backWalk", false);
                }
                   
            }
            else
            {
                // Horizontal movement priority
                anim.SetBool("sidesWalk", true);    // Moving Left or Right
                anim.SetBool("idle", false);
                anim.SetBool("backWalk", false);
                anim.SetBool("walkFront", false);
                // 4. Handle Sprite Flipping without breaking your Inspector Scale
                if (velocity.x > 0.1f)
                {
                    // Face Right: Use original scale
                    transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
                }
                else if (velocity.x < -0.1f)
                {
                    // Face Left: Flip only the X axis of your original scale
                    transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
                }
            }
        }
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackRate;
        PlayerController playerHealth = player.GetComponent<PlayerController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"{gameObject.name} bit you! "+ attackDamage);
        }
    }
}