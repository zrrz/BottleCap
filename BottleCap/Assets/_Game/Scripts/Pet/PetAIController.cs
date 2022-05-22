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
        Wander, Follow, Wait, CollectBottle
    }

    [SerializeField] private State currentState;

    private List<ThrowingBottle> bottlesToPickup = new List<ThrowingBottle>();
    [SerializeField] private GameObject bottleVisual;

    void Start()
    {
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
        SetState(State.Follow);
        //TODO whistle sound
    }

    private void LateUpdate()
    {
        GetState(currentState).LateUpdateState();
    }

    public void ChooseRandomState()
    {
        var newState = (State)Random.Range(0, System.Enum.GetValues(typeof(State)).Length);
        
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
}
