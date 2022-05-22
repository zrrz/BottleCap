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
        Debug.Log("sitting off");
        animator.SetBool("Sitting", false);
    }

    public override void UpdateState()
    {
        Debug.Log("sitting on");
        animator.SetFloat("MoveSpeed", 0f);
        animator.SetBool("Sitting", true);

        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            petAiController.ChooseRandomState();
        }

        //base.UpdateState();
    }
}
