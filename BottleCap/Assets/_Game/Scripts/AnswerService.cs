using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnswerService : MonoBehaviour
{
    public string WEB_URL = "";

    private static AnswerService instance;

    public bool useLocalAnswers = true;
    public TextAsset localAnswersFile;

    private AnswerList answersList;

    private List<AnswerDto> unusedAnswers = new List<AnswerDto>();

    public static bool Ready { get; set; } = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(AnswerService)} already exists");
        }
    }

    void Start()
    {
        _ = LoadAnswersAsync();
    }

    public static AnswerDto GetNewAnswer()
    {
        if(instance.unusedAnswers.Count > 0)
        {
            int index = Random.Range(0, instance.unusedAnswers.Count);
            AnswerDto newAnswer = instance.unusedAnswers[index];
            instance.unusedAnswers.RemoveAt(index);
            return newAnswer;
        }
        else
        {
            Debug.LogError("No unused answers");
            return null;
        }
    }

    public static void SubmitAnswer(AnswerDto answerDto)
    {
        _ = SubmitAnswerAsync(answerDto);
    }

    public static async Task SubmitAnswerAsync(AnswerDto answerDto)
    {
        await RestClient.Instance.PostAsync(instance.WEB_URL, answerDto);
    }

    

    public async Task LoadAnswersAsync()
    {
        if(useLocalAnswers)
        {
            LoadAnswersLocally();
        }
        else
        {
            await LoadAnswersRemoteAsync();
        }

        unusedAnswers = new List<AnswerDto>(answersList.answers);
        Ready = true;
    }

    private void LoadAnswersLocally()
    {
        DeserializeAnswers(localAnswersFile.text);
    }

    private async Task LoadAnswersRemoteAsync()
    {
        await RestClient.Instance.GetAsync(WEB_URL, DeserializeAnswers);
    }

    private void DeserializeAnswers(string jsonData)
    {
        answersList = Newtonsoft.Json.JsonConvert.DeserializeObject<AnswerList>(jsonData);
    }
}
