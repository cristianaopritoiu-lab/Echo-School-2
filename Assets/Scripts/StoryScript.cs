using UnityEngine;
using TMPro; 
using System.Collections;
using System.Collections.Generic;

public class StoryWriter : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; 
    [TextArea(3, 10)]
    public string[] sentences; 
    public float typingSpeed = 0.05f; 

    private int index;
    private bool isTyping;

    void Start()
    {
        
        StartCoroutine(TypeSentence());
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
        
        if (isTyping) return;

        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence());
        }
        else
        {
            textDisplay.text = "At the moment... (Press Go Back)";
        }
    }
}