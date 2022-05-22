using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public static DialogueManager Instance { get; internal set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(DialogueManager)} already exists");
        }
    }

    public void ShowText(string text)
    {
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.SetText(text);
    }
}
