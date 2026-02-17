using UnityEngine;
using TMPro;
public class NectarTracker : MonoBehaviour
{
    PlayerController player;
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text=player.nectarCollected.ToString();
    }
}
