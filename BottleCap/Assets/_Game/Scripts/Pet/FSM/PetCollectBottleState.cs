using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetCollectBottleState : PetBaseState
{
    //float followTimer = 0f;
    //float minFollowTime = 5f;
    //float maxFollowTime = 15f;

    private enum State
    {
        Retrieving, DroppingOff, Watching
    }

    private State state = State.Retrieving;

    private List<ThrowingBottle> bottles;

    private ThrowingBottle heldBottle;
    private float watchTimer;
    private float minWatchTime = 4f;
    private float maxWatchTime = 6f;

    public PetCollectBottleState(List<ThrowingBottle> bottles, NavMeshAgent agent, Animator animator, PetAIController petAiController) 
        : base(agent, animator, petAiController) 
    {
        this.bottles = bottles;
    }

    public override void EnterState()
    {
        state = State.Retrieving;
        //followTimer = Random.Range(minFollowTime, maxFollowTime);
        SetMoveSpeed(maxMoveSpeed);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        switch (state)
        {
            case State.Retrieving:
                HandleRetrievingState();
                break;
            case State.DroppingOff:
                HandleDroppingOffState();
                break;
            case State.Watching:
                HandleWatchingState();
                break;
            default:
                break;
        }
    }

    private void HandleRetrievingState()
    {
        if(ValidateBottleList() == false)
        {
            return;
        }

        var bottleTarget = bottles[0];
        destination = bottleTarget.transform.position;
        navMeshAgent.SetDestination(destination);

        base.UpdateState();
        if (Vector3.Distance(navMeshAgent.transform.position, destination) < 0.8f)
        {
            state = State.DroppingOff;
            PickupBottle();
        }
    }

    private void PickupBottle()
    {
        heldBottle = bottles[0];
        bottles.RemoveAt(0);
        heldBottle.gameObject.SetActive(false);
        petAiController.SetBottleVisuals(true);
    }

    private void HandleDroppingOffState()
    {
        destination = WorldManager.GetBottleDropOffPoint().position;
        navMeshAgent.SetDestination(destination);

        base.UpdateState();
        if (Vector3.Distance(navMeshAgent.transform.position, destination) < 0.4f)
        {
            DropOffBottle();
        }
    }

    private void DropOffBottle()
    {
        heldBottle.transform.position = destination;
        heldBottle.gameObject.SetActive(true);
        heldBottle.HandleTouchedWater(false);
        heldBottle = null;
        petAiController.SetBottleVisuals(false);
        state = State.Watching;
        watchTimer = Random.Range(minWatchTime, maxWatchTime);
    }

    private void HandleWatchingState()
    {
        Debug.Log("watchTimer: " + watchTimer);
        watchTimer -= Time.deltaTime;
        if(watchTimer >= 0f)
        {
            animator.SetBool("Sitting", true);
        }
        else
        {
            animator.SetBool("Sitting", false);
            petAiController.StartCoroutine(EndWatching(0.3f));
        }
    }

    private IEnumerator EndWatching(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(ValidateBottleList())
        {
            state = State.Retrieving;
        }
    }

    private bool ValidateBottleList()
    {
        for(int i = 0; i < bottles.Count; i++)
        {
            if(bottles[i] == null)
            {
                bottles.RemoveAt(i);
                i--;
                continue;
            }
            if(bottles[i].WaterTouched)
            {
                bottles.RemoveAt(i);
                i--;
            }
        }

        if (bottles.Count == 0)
        {
            petAiController.ChooseRandomState();
            return false;
        }
        return true;
    }
}
