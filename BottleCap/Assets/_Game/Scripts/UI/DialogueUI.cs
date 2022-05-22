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
    }

    public void CloseText()
    {
        //TODO shrink and grow box
        PlayerData.ReleaseInputLock();
        gameObject.SetActive(false);
    }
}
