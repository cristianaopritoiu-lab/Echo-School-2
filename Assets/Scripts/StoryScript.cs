using UnityEngine;
using TMPro;
using System.Collections;

public class StoryWriter : MonoBehaviour
{
    [Header("Referinte UI")]
    public TextMeshProUGUI textDisplay;
    public GameObject nextButton;

    [Header("Setari Poveste")]
    [TextArea(3, 10)]
    public string[] sentences;
    public float typingSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine; 

    void Start()
    {
        if (textDisplay == null)
        {
            Debug.LogError("Lipseste referinta catre TextDisplay pe obiectul " + gameObject.name);
            return;
        }

        typingCoroutine = StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        isTyping = true;
        textDisplay.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextSentence()
    {
        
        if (isTyping)
        {
            StopCoroutine(typingCoroutine); 
            textDisplay.text = sentences[index]; 
            isTyping = false;
            return; 
        }

        if (index < sentences.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeSentence());
        }
        else
        {
            textDisplay.text = "Press on Go Back... (wasd front, back, left, right; space and face the enemy for attack; press E to talk)";
            if (nextButton != null)
                nextButton.SetActive(false);
        }
    }
}