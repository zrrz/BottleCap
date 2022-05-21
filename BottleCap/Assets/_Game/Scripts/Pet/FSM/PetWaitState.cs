using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetWaitState : PetBaseState
{
    float waitTimer = 0f;
    float minWaitTime = 4f;
    float maxWaitTime = 10f;

    public PetWaitState(NavMeshAgent agent, Animator animator, PetAIController petAiController) 
        : base(agent, animator, petAiController) { }

    public override void EnterState()
    {
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            petAiController.ChooseRandomState();
        }

        animator.SetFloat("MoveSpeed", 0f);

        //base.UpdateState();
    }
}
