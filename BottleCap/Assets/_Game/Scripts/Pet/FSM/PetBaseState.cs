using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class PetBaseState : FSMState
{
    protected NavMeshAgent navMeshAgent;
    protected PetAIController petAiController;
    protected Vector3 destination;

    public PetBaseState(NavMeshAgent navMeshAgent, PetAIController petAiController)
    {
        this.navMeshAgent = navMeshAgent;
        this.petAiController = petAiController;
    }

    public override void UpdateState()
    {
        MoveTowardsDestination();
    }

    protected void MoveTowardsDestination()
    {
        navMeshAgent.isStopped = true;

        Transform transform = petAiController.transform;

        Vector3[] corners = navMeshAgent.path.corners;

        for(int i = 0; i < corners.Length-1; i++)
        {
            Debug.DrawLine(corners[i], corners[i + 1], Color.magenta);
        }
        Debug.DrawLine(corners[corners.Length-1], destination, Color.magenta);

        Vector3 targetPos = Vector3.zero;
        if (corners.Length > 1)
        {
            int index = Mathf.Min(corners.Length - 1, 1);
            Vector3 nextPosition = corners[index];

            targetPos = nextPosition;
        }
        else
        {
            targetPos = destination;
        }

        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        float turnSpeed = petAiController.turnSpeed;
        //if (Vector3.Dot(transform.rotation.eulerAngles, lookRot.eulerAngles) < 0.9f)
        //{
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, turnSpeed * Time.deltaTime);
        //}
        
        navMeshAgent.Move(transform.forward * Time.deltaTime * navMeshAgent.speed);
    }
}
