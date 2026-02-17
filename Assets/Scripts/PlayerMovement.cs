using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input in Update
        float x = 0;
        float y = 0;
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed) x = -1f;
            if (keyboard.dKey.isPressed) x = 1f;
            if (keyboard.wKey.isPressed) y = 1f;
            if (keyboard.sKey.isPressed) y = -1f;
        }

        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        // Move the physical body in FixedUpdate for smooth collisions
        rb.linearVelocity = moveInput * speed;
    }
}