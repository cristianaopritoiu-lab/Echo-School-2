using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
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
        // Call a separate function to handle animations
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        // 2. Check moveInput to see which one to turn back on
        if (moveInput.magnitude == 0)
        {
            anim.SetBool("idle", true);
            anim.SetBool("backWalk", false);
            anim.SetBool("sidesWalk", false);
            anim.SetBool("walkFront", false);
        }
        else if (moveInput.y > 0) // Moving Up
        {
            anim.SetBool("backWalk", true);
            anim.SetBool("walkFront", false);
            anim.SetBool("idle", false);
            anim.SetBool("sidesWalk", false);
        }
        else if (moveInput.y < 0) // Moving Down
        {
            anim.SetBool("walkFront", true);
            anim.SetBool("backWalk", false);
            anim.SetBool("sidesWalk", false);
            anim.SetBool("idle", false);
        }
        else if (moveInput.x != 0) // Moving Left or Right
        {
            anim.SetBool("sidesWalk", true);
            anim.SetBool("backWalk", false);
            anim.SetBool("idle", false);
            anim.SetBool("walkFront", false);
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    void FixedUpdate()
    {
        // Move the physical body in FixedUpdate for smooth collisions
        rb.linearVelocity = moveInput * speed;
    }
}