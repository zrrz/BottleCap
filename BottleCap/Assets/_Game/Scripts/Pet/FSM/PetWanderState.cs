using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetWanderState : PetBaseState
{
    //public Vector3 Destination { get; private set; }

    public PetWanderState(NavMeshAgent agent, Animator animator, PetAIController petAiController) 
        : base(agent, animator, petAiController) { }

    public override void EnterState()
    {
        SetMoveSpeed(minMoveSpeed);
        ChooseNewDestination();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        navMeshAgent.SetDestination(destination);

        if (Vector3.Distance(navMeshAgent.transform.position, destination) < 0.14f)
        {
            //petAiController.ChooseRandomState();
            petAiController.SetState(PetAIController.State.Wait);
        }

        base.UpdateState();
    }

    private void ChooseNewDestination()
    {
        Vector3 worldCenter = WorldManager.GetWorldCenter();
        float radius = WorldManager.GetWorldRadius();
        Vector3 randomPos = worldCenter + (Random.insideUnitSphere * radius);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, 1))
        {
            destination = hit.position;
        }
        else
        {
            Debug.LogError("Hit nothing");
        }
    }
}
