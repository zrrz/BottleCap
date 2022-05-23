using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetFollowState : PetBaseState
{
    public Transform Target { get; set; }

    float followTimer = 0f;
    float minFollowTime = 5f;
    float maxFollowTime = 15f;

    public PetFollowState(NavMeshAgent agent, Animator animator, PetAIController petAiController) 
        : base(agent, animator, petAiController) { }

    public override void EnterState()
    {
        //SetMoveSpeed(Random.Range(minMoveSpeed, maxMoveSpeed));
        SetMoveSpeed(maxMoveSpeed);
        followTimer = Random.Range(minFollowTime, maxFollowTime);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        navMeshAgent.SetDestination(Target.position);
        destination = Target.position;

        if (Vector3.Distance(navMeshAgent.transform.position, Target.position) > 3f)
        {
             base.UpdateState();
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0f);
        }

        followTimer -= Time.deltaTime;
        if(followTimer <= 0f)
        {
            petAiController.ChooseRandomState();
        }
    }
}
