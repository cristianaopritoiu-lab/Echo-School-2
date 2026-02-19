using UnityEngine;
using TMPro;
public class SoundToggle : MonoBehaviour
{
   [SerializeField] AudioSource music;
    [SerializeField] AudioSource playerSound;
    TextMeshProUGUI text;
   public bool isOn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Sound On";
    }

    // Update is called once per frame
   public void SoundToggler()
    {
        isOn = !isOn;
        if (isOn) { music.volume = 0.10f; playerSound.volume = 0.25f; text.text = "Sound On"; }
        else { music.volume = 0f; playerSound.volume = 0f; text.text = "Sound Off"; }

    }
}
