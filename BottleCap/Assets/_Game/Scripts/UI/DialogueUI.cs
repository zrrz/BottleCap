using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI dialogueText;

    [SerializeField] private UnityEngine.UI.Button closeButton;

    private static DialogueUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(DialogueUI)} already exists");
        }

        closeButton.onClick.AddListener(CloseText);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CloseText();
        }
    }

    public static void SetText(string text)
    {
        instance.gameObject.SetActive(true);
        PlayerData.AddInputLock();
        instance.dialogueText.text = text;
        instance.StartCoroutine(instance.AnimateScale(Vector3.one * 0.01f, Vector3.one, 0.5f, null));
    }

    public static void CloseText()
    {      
        PlayerData.ReleaseInputLock();
        instance.StartCoroutine(instance.AnimateScale(Vector3.one, Vector3.one * 0.01f, 0.5f, () =>
        {
            instance.gameObject.SetActive(false);
            TutorialManager.Instance.ShowObjectText();
            TutorialManager.Instance.TriggerEventCompleted(TutorialManager.TutorialSection.FinishTutorial);
            TutorialManager.Instance.TriggerEventCompleted(TutorialManager.TutorialSection.House);
        }
        ));
    }

    public IEnumerator AnimateScale(Vector3 startSize, Vector3 endSize, float time, System.Action OnComplete)
    {
        for(float t = 0; t <= 1f; t += Time.deltaTime/time)
        {
            transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
        if(OnComplete != null)
        {
            OnComplete.Invoke();
        }
    }
}
