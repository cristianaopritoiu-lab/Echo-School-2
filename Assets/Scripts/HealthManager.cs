using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBarFill;   
    public PlayerController player; 

    private float maxHealth;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
           
            maxHealth = player.health;
        }
    }

    void Update()
    {
        if (player != null && healthBarFill != null)
        {
          
            healthBarFill.fillAmount = (float)player.health / maxHealth;
        }
    }
}