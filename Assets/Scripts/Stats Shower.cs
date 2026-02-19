using UnityEngine;
using TMPro;
public class StatsShower : MonoBehaviour
{
    PlayerController player;
   [SerializeField] TextMeshProUGUI health;
   [SerializeField] TextMeshProUGUI damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.health.ToString();
        damage.text = player.damage.ToString();
    }
}
