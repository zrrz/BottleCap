using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI dialogueText;

    [SerializeField] private UnityEngine.UI.Button closeButton;

    private void Awake()
    {
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

    public void SetText(string text)
    {
        PlayerData.AddInputLock();
        dialogueText.text = text;
        StartCoroutine(AnimateScale(Vector3.one * 0.01f, Vector3.one, 0.5f, null));
    }

    public void CloseText()
    {
        PlayerData.ReleaseInputLock();
        StartCoroutine(AnimateScale(Vector3.one, Vector3.one * 0.01f, 0.5f, () =>
        {
            gameObject.SetActive(false);
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
