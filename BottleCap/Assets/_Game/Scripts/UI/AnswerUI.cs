using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerUI : MonoBehaviour
{
    public static AnswerUI Instance;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI answerText;
    public GameObject answerUi;
    Animator animator;
    float startTime;




    [SerializeField] private UnityEngine.UI.Button closeButton;

    
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
        closeButton.onClick.AddListener(()=> { gameObject.SetActive(false); });

        animator = gameObject.gameObject.GetComponent<Animator>();

        

    }
    void OnEnable()
    {
        startTime = Time.time;
    }

  
    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {              
                animator.SetBool("Close", true);
                Disable();
            }

        }
    }

    public void SetText(string promptText, string answerText)
    {
        this.promptText.text = promptText;
        this.answerText.text = answerText;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        if (Time.time > startTime + 5)
        {
            answerUi.SetActive(false);
            Debug.Log("Delete Message");
        }

    }
}
