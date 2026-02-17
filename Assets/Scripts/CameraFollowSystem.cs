using UnityEngine;

public class CameraFollowSystem : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    // In 2D, -10 is the standard depth to see your sprites
    public float zOffset = -10f;

    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            // We follow X and Y, but keep our own Z distance
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, zOffset);

            // Smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothSpeed);

            // Keep rotation at 0 for 2D!
            transform.rotation = Quaternion.identity;
        }
    }
}