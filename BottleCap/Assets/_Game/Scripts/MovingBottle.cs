using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBottle : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private float speed = 1f;

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
