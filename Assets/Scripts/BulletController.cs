using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 40f;

    public int damageToDeal;
    private Vector2 startPosition; // Changed to Vector2 for 2D

    void Start()
    {
        startPosition = transform.position;
        // Optimization: It's better to pass damage via Setup() when spawning 
        // than to use FindFirstObjectByType every time a bullet is born.
        var player = FindFirstObjectByType<PlayerController>();
        if (player != null) damageToDeal = player.damage;
    }

    void Update()
    {
        // FIX: Use transform.right (X-axis) instead of Vector3.forward (Z-axis)
        // This moves the bullet in the direction its "nose" is pointing in 2D space.
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // FIX: Use OnTriggerEnter2D for 2D physics
    private void OnTriggerEnter2D(Collider2D other)
    {
        ZombieController zombie = other.GetComponent<ZombieController>();

        if (zombie != null)
        {
            zombie.TakeDamage(damageToDeal);
        }

        Destroy(gameObject);
    }
}