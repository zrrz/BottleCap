using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnswerService : MonoBehaviour
{
    public string LOCAL_URL = "https://18.218.91.1:5001/answer";
    public string WEB_URL = "http://18.223.69.65:5001/answer";
    public bool useRemoteUrl = true;

    private static AnswerService instance;

    //public bool useLocalAnswers = true;
    //public TextAsset localAnswersFile;

    private List<AnswerDto> answersList = new List<AnswerDto>();

    //private List<AnswerDto> unusedAnswers = new List<AnswerDto>();

    public static bool Ready { get; set; } = false;
    public static int AnswerCount => instance.answersList.Count;

    private const int GET_NEW_ANSWER_THRESHOLD = 3;

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
        LoadAnswersAsync();
    }

    //private void OnGUI()
    //{
    //    if(GUILayout.Button(""))
    //}

    public static AnswerDto GetNewAnswer()
    {
        if(instance.answersList.Count > 0)
        {
            int index = Random.Range(0, instance.answersList.Count);
            AnswerDto newAnswer = instance.answersList[index];
            instance.answersList.RemoveAt(index);
            if(instance.answersList.Count <= GET_NEW_ANSWER_THRESHOLD)
            {
                Debug.Log("Almost out of answers. Pulling new ones from server");
                instance.LoadAnswersAsync();
            }
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
        SubmitAnswerAsync(answerDto);
    }

    public static async void SubmitAnswerAsync(AnswerDto answerDto)
    {
        string url = instance.useRemoteUrl ? instance.WEB_URL : instance.LOCAL_URL;
        await RestClient.Instance.PostAsync(url, answerDto);
    }

    public async void LoadAnswersAsync()
    {
        //if(useLocalAnswers)
        //{
        //    LoadAnswersLocally();
        //}
        //else
        {
            await LoadAnswersRemoteAsync();
        }

        Ready = true;
    }

    //private void LoadAnswersLocally()
    //{
    //    DeserializeAnswers(localAnswersFile.text);
    //}

    private async Task LoadAnswersRemoteAsync()
    {
        string url = instance.useRemoteUrl ? instance.WEB_URL : instance.LOCAL_URL;
        await RestClient.Instance.GetAsync(url, DeserializeAnswers);
    }

    private void DeserializeAnswers(string jsonData)
    {
        var newAnswers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AnswerDto>>(jsonData);
        answersList.AddRange(newAnswers);
    }
}
