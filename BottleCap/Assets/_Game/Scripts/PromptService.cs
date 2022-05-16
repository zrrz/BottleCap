using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptService : MonoBehaviour
{
    private static PromptService instance;

    public bool useLocalAnswers = true;
    public TextAsset localPromptListFile;

    private PromptList promptList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(PromptService)} already exists");
        }

        LoadPrompts();
    }

    public static PromptList GetPromptList()
    {
        return instance.promptList;
    }

    public void LoadPrompts()
    {
        if(useLocalAnswers)
        {
            LoadPromptsLocally();
        }
        else
        {
            LoadPromptsRemote();
        }
    }

    private void LoadPromptsLocally()
    {
        promptList = Newtonsoft.Json.JsonConvert.DeserializeObject<PromptList>(localPromptListFile.text);
    }

    private void LoadPromptsRemote()
    {
        //TODO
    }
}
