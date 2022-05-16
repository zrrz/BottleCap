using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptInputUI : MonoBehaviour
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

    public void SetText(string promptText)
    {
        this.promptText.text = promptText;
        gameObject.SetActive(true);
        answerField.text = "";
        answerField.Select();
        answerField.ActivateInputField();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void SubmitPrompt()
    {
        AnswerService.SubmitAnswer(new AnswerDto
        {
            prompt = promptText.text,
            answer = answerField.text,
            author = "zrrz",
            dateTime = System.DateTime.Now
        });
        gameObject.SetActive(false);
    }
}
