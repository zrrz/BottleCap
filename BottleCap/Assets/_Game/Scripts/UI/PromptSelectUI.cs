using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptSelectUI : GenericPageUI
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
        AddRandomPrompt();
        var promptList = PromptService.GetPromptList();
        PopulatePrompts(promptList);
        gameObject.SetActive(false);

        //contentHolder.sizeDelta

        cancelButton.onClick.AddListener(Cancel);
    }

    private void AddRandomPrompt()
    {
        var option = Instantiate(promptOptionPrefab, contentHolder);
        option.Initialize("Random", (str)=>
        {
            string randomPrompt = PromptService.GetPromptList().prompts.RandomItem();
            SelectPrompt(randomPrompt);
        });
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
        Close();
    }

    public void SelectPrompt(string prompt)
    {
        promptInputUI.Open(prompt);
        Close();
    }
}
