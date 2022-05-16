using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerService : MonoBehaviour
{
    public bool useLocalAnswers = true;
    public TextAsset localAnswersFile;

    [SerializeField]
    private AnswerList answers;

    Queue<AnswerDto> unusedAnswers = new Queue<AnswerDto>();

    void Start()
    {
        LoadAnswers();
        unusedAnswers = new Queue<AnswerDto>(answers.answers);
    }

    void Update()
    {
        
    }

    public AnswerDto GetNewAnswer()
    {
        if(unusedAnswers.Count > 0)
        {
            AnswerDto newAnswer = unusedAnswers.Dequeue();
            return newAnswer;
        }
        else
        {
            Debug.LogError("No unused answers");
        }
        return null;
    }

    public void LoadAnswers()
    {
        if(useLocalAnswers)
        {
            LoadAnswersLocally();
        }
        else
        {
            LoadAnswersRemote();
        }
    }

    private void LoadAnswersLocally()
    {
        answers = Newtonsoft.Json.JsonConvert.DeserializeObject<AnswerList>(localAnswersFile.text);
    }

    private void LoadAnswersRemote()
    {
        //TODO
    }
}
