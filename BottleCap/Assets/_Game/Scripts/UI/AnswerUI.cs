using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerUI : MonoBehaviour
{
    public static AnswerUI Instance;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI answerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(AnswerUI)} already exists");
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetText(string promptText, string answerText)
    {
        this.promptText.text = promptText;
        this.answerText.text = answerText;
        gameObject.SetActive(true);
    }
}
