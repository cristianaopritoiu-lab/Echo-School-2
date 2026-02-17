using UnityEngine;
using TMPro;

public class NectarDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public PlayerController player;
    public int nectarTarget = 10;

    
    public Color culoareNormala = Color.white;
    public Color culoareFinala = Color.green;

    void Update()
    {
        if (player != null)
        {
            textMesh.text = "NECTAR: " + player.nectarCollected + " / " + nectarTarget;

            if (player.nectarCollected >= nectarTarget)
            {
                textMesh.color = culoareFinala;
                textMesh.fontSize = 30; 
            }
            else
            {
                textMesh.color = culoareNormala;
            }
        }
    }
}