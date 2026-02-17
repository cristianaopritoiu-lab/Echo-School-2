using UnityEngine;

public class Nectar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().nectarCollected++;
        Destroy(gameObject);
    }
}
