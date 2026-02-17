using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int health = 250;
    public int damage = 10;
    public float attackSpeed = 1.0f;
    public int nectarDropAmount = 2;
    public GameObject nectarPrefab;
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Zombie mort! Ai primit: " + nectarDropAmount + " nectar");

        for (int i =0; i < nectarDropAmount; i++)
        {
            Instantiate(nectarPrefab, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}
