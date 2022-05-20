using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBottle : Interactable
{
    [SerializeField] private float speed = 1f;
    private AnswerDto answerData;

    private Vector3 targetPosition;

    private System.Action FreeBottleSpawnEvent;

    public void Initialize(Vector3 position, AnswerDto answerDto, System.Action FreeBottleSpawnEvent)
    {
        targetPosition = position;
        answerData = answerDto;
        this.FreeBottleSpawnEvent = FreeBottleSpawnEvent;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public override void Interact(PlayerData playerData)
    {
        string answerText = answerData.answer;
        string promptText = answerData.prompt;
        AnswerUI.Instance.SetText(promptText, answerText);
        FreeBottleSpawnEvent.Invoke();
        Destroy(gameObject);
    }
}
