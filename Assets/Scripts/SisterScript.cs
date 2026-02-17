using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; 

public class Sister : MonoBehaviour
{
    [Header("Configurare UI")]
    public GameObject dialoguePanel; 
    public TextMeshProUGUI dialogueText; 

    [Header("Setari Dialog")]
    public string[] dialogue;
    private int index = 0;
    public float wordSpeed = 0.05f;

    [Header("Detectie Jucator")]
    public bool playerIsClose;

    void Start()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false); 
    }

    void Update()
    {
        
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }

        
        if (Keyboard.current.qKey.wasPressedThisFrame && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false); 
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText(); 
        }
    }
}