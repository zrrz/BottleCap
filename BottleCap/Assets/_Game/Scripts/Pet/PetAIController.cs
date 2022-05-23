using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAIController : MonoBehaviour
{
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] public float turnSpeed = 360f;
    [SerializeField] public Animator animator;

    private Dictionary<State, PetBaseState> stateMap = new Dictionary<State, PetBaseState>();

    public enum State
    {
        Wander, Follow, Wait, CollectBottle, Interact
    }

    [SerializeField] private State currentState;

    private List<ThrowingBottle> bottlesToPickup = new List<ThrowingBottle>(); 

    [SerializeField] private GameObject bottleVisual;

    [SerializeField] private ParticleSystem loveVFX;

    void Start()
    {
        animator.SetBool("Sitting", false);

        SetBottleVisuals(false);

        var petFollowState = new PetFollowState(agent, animator, this);
        petFollowState.Target = FindObjectOfType<PlayerData>().transform;
        stateMap.Add(State.Follow, petFollowState);

        var petWanderState = new PetWanderState(agent, animator, this);
        stateMap.Add(State.Wander, petWanderState);

        var petWaitState = new PetWaitState(agent, animator, this);
        stateMap.Add(State.Wait, petWaitState);

        var petCollectBottleState = new PetCollectBottleState(bottlesToPickup, agent, animator, this);
        stateMap.Add(State.CollectBottle, petCollectBottleState);

        var petInteractState = new PetInteractState(agent, animator, this);
        stateMap.Add(State.Interact, petInteractState);

        SetState(State.Wander);
    }

    void Update()
    {
        GetState(currentState).UpdateState();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CallDogOver();
        }
    }

    public void CallDogOver()
    {
        if(currentState == State.CollectBottle)
        {
            return;
        }
        SetState(State.Follow);
    }

    public void InteractWithDog()
    {
        if (currentState == State.CollectBottle || currentState == State.Interact)
        {
            return;
        }
        SetState(State.Interact);
    }

    private void LateUpdate()
    {
        GetState(currentState).LateUpdateState();
    }

    public void ChooseRandomState()
    {
        State newState;
        do
        {
            newState = (State)Random.Range(0, System.Enum.GetValues(typeof(State)).Length);
        } while (newState == State.CollectBottle || newState == State.Interact);

        SetState(newState);
    }

    public void SetState(State newState)
    {
        GetState(currentState).ExitState();
        //print($"new state: {newState}");
        currentState = newState;

        GetState(newState).EnterState();
    }

    public void SetFollowTarget()
    {
        SetState(State.Follow);
    }

    public void SetBottleTarget(ThrowingBottle bottle)
    {
        bottlesToPickup.Add(bottle);
        if(currentState != State.CollectBottle)
        {
            SetState(State.CollectBottle);
        }
    }

    private PetBaseState GetState(State state)
    {
        if(stateMap.TryGetValue(state, out PetBaseState outState))
        {
            return outState;
        }
        else
        {
            Debug.LogError($"No State {state}");
            return null;
        }
    }

    public void SetBottleVisuals(bool active)
    {
        bottleVisual.SetActive(active);
    }

    public void SetLoveVFXActive(bool active)
    {  
        if(active)
        {
            loveVFX.Play();
        }
        else
        {
            loveVFX.Stop();
        }
    }
}
