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

    void Start()
    {
        if (textDisplay == null)
        {
            Debug.LogError("Lipseste referinta catre TextDisplay pe obiectul " + gameObject.name);
            return;
        }

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
        Debug.Log("Am terminat de scris propozitia: " + index);
    }

    public void NextSentence()
    {
        Debug.Log("Butonul Next a fost apasat fizic!");

        if (isTyping)
        {
            Debug.Log("Still writing");
            return;
        }

        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence());
        }
        else
        {
            textDisplay.text = "Press on Go Back...";
            Debug.Log("Done");

            if (nextButton != null)
                nextButton.SetActive(false); 
        }
    }
}