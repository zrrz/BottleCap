using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerService : MonoBehaviour
{
    public bool useLocalAnswers = true;
    public TextAsset localAnswersFile;

    [SerializeField] private AnswerList answers;

    private List<AnswerDto> unusedAnswers = new List<AnswerDto>();

    void Start()
    {
        LoadAnswers();
        unusedAnswers = new List<AnswerDto>(answers.answers);
    }

    void Update()
    {
        
    }

    public AnswerDto GetNewAnswer()
    {
        if(unusedAnswers.Count > 0)
        {
            int index = Random.Range(0, unusedAnswers.Count);
            AnswerDto newAnswer = unusedAnswers[index];
            unusedAnswers.RemoveAt(index);
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
