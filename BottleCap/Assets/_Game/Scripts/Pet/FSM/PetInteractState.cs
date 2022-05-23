using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetInteractState : PetBaseState
{
    float interactTimer = 0f;
    float minInteractTime = 1.8f;
    float maxInteractTime = 1.8f;

    public PetInteractState(NavMeshAgent agent, Animator animator, PetAIController petAiController)
        : base(agent, animator, petAiController) { }

    public override void EnterState()
    {
        //turn off collider so sign goes away
        petAiController.GetComponent<Collider>().enabled = false;

        GameObject.FindObjectOfType<PlayerAnimator>().PlayPickup();
        PlayerData.AddInputLock();
        animator.SetBool("Play", true);
        animator.SetFloat("MoveSpeed", 0f);

        interactTimer = Random.Range(minInteractTime, maxInteractTime);
        petAiController.SetLoveVFXActive(true);
    }

    public override void ExitState()
    {
        petAiController.GetComponent<Collider>().enabled = true;
        animator.SetBool("Play", false);
        petAiController.SetLoveVFXActive(false);
        PlayerData.ReleaseInputLock();
    }

    public override void UpdateState()
    {
        interactTimer -= Time.deltaTime;
        if (interactTimer <= 0f)
        {
            //petAiController.ChooseRandomState();
            petAiController.SetState(PetAIController.State.Follow);
        }
    }
}
