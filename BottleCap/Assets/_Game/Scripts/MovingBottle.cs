using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBottle : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private float speed = 1f;
    public AnswerDto answerData;

    public void Initialize(Vector3 position, AnswerDto answerdto)
    {
        targetPosition = position;
        answerData = answerdto;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
