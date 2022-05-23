using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PromptInputUI : GenericPageUI
{
    public static PromptInputUI Instance;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TMP_InputField answerField;

    [SerializeField] private UnityEngine.UI.Button cancelButton;
    [SerializeField] private UnityEngine.UI.Button submitButton;
    [SerializeField] private UnityEngine.UI.Button signatureButton;

    private bool signed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(PromptInputUI)} already exists");
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
        cancelButton.onClick.AddListener(Cancel);
        submitButton.onClick.AddListener(SubmitPrompt);
        signatureButton.onClick.AddListener(SignSignature);
        CheckCharacterCount("");
    }

    private void SignSignature()
    {
        signatureButton.interactable = false;
        signatureButton.GetComponentInChildren<TextMeshProUGUI>().text = UserManager.GetUserName();
        signatureButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
        signed = true;
        CheckCharacterCount(answerField.text);
    }

    public void Open(string promptText)
    {
        Open();

        this.promptText.text = promptText;
        answerField.text = "";
        answerField.Select();
        answerField.ActivateInputField();
        answerField.onValueChanged.AddListener(CheckCharacterCount);
    }

    private void CheckCharacterCount(string text)
    {
        submitButton.interactable = text.Length >= 2 && signed;
    }

    public void Cancel()
    {
        Close();
    }

    public void SubmitPrompt()
    {
        var newAnswer = new AnswerDto
        {
            prompt = promptText.text,
            answer = answerField.text,
            author = UserManager.GetUserName(),
            dateTime = System.DateTime.Now
        };

        TutorialManager.Instance.TriggerEventCompleted(TutorialManager.TutorialSection.HouseTwo);

        PlayerData.Instance.PlayerBottleHolder.SetHeldMessage(newAnswer);
        Close();
    }
}
