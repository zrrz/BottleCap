using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptInputUI : GenericPageUI
{
    public static PromptInputUI Instance;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TMP_InputField answerField;

    [SerializeField] private UnityEngine.UI.Button cancelButton;
    [SerializeField] private UnityEngine.UI.Button submitButton;

    //TODO only submit if response was written

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
    }

    public void Open(string promptText)
    {
        Open();

        this.promptText.text = promptText;
        answerField.text = "";
        answerField.Select();
        answerField.ActivateInputField();
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
            author = System.Environment.UserName,
            dateTime = System.DateTime.Now
        };
        //AnswerService.SubmitAnswer(newAnswer);
  
        PlayerData.Instance.PlayerBottleHolder.SetHeldMessage(newAnswer);
        Close();
    }
}
