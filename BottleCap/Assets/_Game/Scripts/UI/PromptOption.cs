using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptOption : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI promptText;
    [SerializeField] private UnityEngine.UI.Button selectButton;

    public void Initialize(string prompt, System.Action<string> selectPrompt)
    {
        promptText.text = prompt;
        selectButton.onClick.AddListener(() => { selectPrompt.Invoke(prompt); });
    }
}
