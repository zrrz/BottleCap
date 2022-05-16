using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptSelectUI : MonoBehaviour
{
    public static PromptSelectUI Instance;

    [SerializeField] private PromptOption promptOptionPrefab;
    [SerializeField] private RectTransform contentHolder;

    [SerializeField] private PromptInputUI promptInputUI;

    [SerializeField] private UnityEngine.UI.Button cancelButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(PromptSelectUI)} already exists");
        }
    }

    private void Start()
    {
        var promptList = PromptService.GetPromptList();
        PopulatePrompts(promptList);
        gameObject.SetActive(false);

        cancelButton.onClick.AddListener(Cancel);
    }

    public void PopulatePrompts(PromptList prompts)
    {
        foreach(string prompt in prompts.prompts)
        {
            var option = Instantiate(promptOptionPrefab, contentHolder);
            option.Initialize(prompt, SelectPrompt);
        }
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void SelectPrompt(string prompt)
    {
        promptInputUI.SetText(prompt);
        gameObject.SetActive(false);
    }
}
