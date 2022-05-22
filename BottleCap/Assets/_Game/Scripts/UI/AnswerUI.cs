using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerUI : GenericPageUI
{
    public static AnswerUI Instance;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI answerText;
    public TextMeshProUGUI authorText;
    public GameObject answerUi;
    Animator animator;
    public AudioSource m_MyAudioSource;

    [SerializeField] private UnityEngine.UI.Button closeButton;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private bool opening;

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
        closeButton.onClick.AddListener(()=> { AnimateClosePage(); });

        animator = gameObject.gameObject.GetComponent<Animator>();

        m_MyAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            //HACK to make sure you dont close it until its done opening to prevent quick double tap
            if (Input.GetKeyDown(KeyCode.F))
            {
                AnimateClosePage();
            }
        }
    }

    private void AnimateOpenPage()
    {
        opening = true;
        StartCoroutine(PlayAnimWaitAndInvoke("AnswerUiOpen", 1f, ()=> { opening = false; }));
        StartCoroutine(PlayDelayedSound(openSound, 0f));
    }

    private void AnimateClosePage()
    {
        
        StartCoroutine(PlayAnimWaitAndInvoke("AnswerUiClose", 0.6f, ()=> { 
            PlayerData.ReleaseInputLock(); 
            gameObject.SetActive(false);
            TutorialManager.Instance.TriggerEventCompleted(TutorialManager.TutorialSection.FindFirstBottle);
        }));
        StartCoroutine(PlayDelayedSound(closeSound, 0f));
    }

    public IEnumerator PlayAnimWaitAndInvoke(string animName, float waitTime, System.Action invokeEvent)
    {
        animator.Play(animName);
        animator.SetBool("Close", true);
        if(invokeEvent != null)
        {
            yield return new WaitForSeconds(waitTime);
            invokeEvent.Invoke();
        }
    }

    IEnumerator PlayDelayedSound(AudioClip sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_MyAudioSource.clip = sound;
        m_MyAudioSource.Play();
    }

    public void SetText(string promptText, string answerText, string authorText)
    {
        //Open();

        this.promptText.text = promptText;
        this.answerText.text = answerText;
        this.authorText.text = authorText;

        gameObject.SetActive(true);
        AnimateOpenPage();
    } 
}
