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
    public AudioSource m_MyAudioSource;




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

        m_MyAudioSource = GetComponent<AudioSource>();

    }

    public IEnumerator PlayAnimWaitAndDisable(string animName, float waitTime)
    {
        animator.SetBool("Close", true);
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    IEnumerator playSoundAfterTenSeconds()
    {
        yield return new WaitForSeconds(1);
        m_MyAudioSource.Play();
    }

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //animator.SetBool("Close", true);
                StartCoroutine(PlayAnimWaitAndDisable("Open", 2f));
                StartCoroutine(playSoundAfterTenSeconds());

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
