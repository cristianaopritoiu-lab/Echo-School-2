using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int nectarCollected = 0;
    public int damage = 40;
    [SerializeField] int maxHealth = 100;
    [Header("Spawn Settings")]
    public float fireOffsetDistance = 0.5f;
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;
    private Vector2 lastMovementDirection = Vector2.right; // Default shooting direction

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) SceneManager.LoadScene("Menu");
    }
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        // 1. Capture Movement Input for Direction
        float moveX = 0;
        float moveY = 0;

        if (Keyboard.current.wKey.isPressed) moveY = 1;
        if (Keyboard.current.sKey.isPressed) moveY = -1;
        if (Keyboard.current.aKey.isPressed) moveX = -1;
        if (Keyboard.current.dKey.isPressed) moveX = 1;

        Vector2 currentInput = new Vector2(moveX, moveY);

        // Update direction only if we are moving, so we don't reset to (0,0) when stopped
        if (currentInput.sqrMagnitude > 0)
        {
            lastMovementDirection = currentInput.normalized;
        }

        // 2. Shooting Logic
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // 1. Calculate the rotation (same as before)
        float angle = Mathf.Atan2(lastMovementDirection.y, lastMovementDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 2. Calculate the spawn position dynamically
        // We take the player's position and add the direction vector multiplied by a distance
        Vector3 spawnPosition = transform.position + (Vector3)(lastMovementDirection * fireOffsetDistance);

        // 3. Spawn at the NEW position
        Instantiate(projectilePrefab, spawnPosition, rotation);
    }
}