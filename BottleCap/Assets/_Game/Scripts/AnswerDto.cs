using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerDto
{
    public string prompt;
    public string answer;
    public string submittedBy;
    public string dateTime;
}

[System.Serializable]
public class AnswerList
{
    public List<AnswerDto> answers;
}