using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerWritingTable : Interactable
{
    public void OpenWritingPrompt()
    {
        PromptSelectUI.Instance.Open();
    }

    public override void Interact(PlayerData playerData)
    {
        if(!PromptSelectUI.Instance.gameObject.activeSelf)
        {
            OpenWritingPrompt();
        }
    }
}
