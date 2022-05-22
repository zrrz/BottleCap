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
        playerData.PlayerAnimator.PlayPickup();

        StartCoroutine(OpenPageAfterDelay(1f));
    }

    IEnumerator OpenPageAfterDelay(float delay)
    {
        GetComponent<Collider>().enabled = false;
        PlayerData.AddInputLock();
        yield return new WaitForSeconds(delay);
        string answerText = answerData.answer;
        string promptText = answerData.prompt;
        string authorText = answerData.author;
        AnswerUI.Instance.SetText(promptText, answerText, authorText);
        FreeBottleSpawnEvent.Invoke();
        Destroy(gameObject);
    }
}
