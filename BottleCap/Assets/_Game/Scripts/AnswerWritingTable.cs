using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerWritingTable : MonoBehaviour
{
    //TODO rewrite pickup logic to more generic interact logic. Have 'f' pickup active table and open writing prompt

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenWritingPrompt();
        }
    }

    public void OpenWritingPrompt()
    {
        PromptSelectUI.Instance.gameObject.SetActive(true);
    }
}
