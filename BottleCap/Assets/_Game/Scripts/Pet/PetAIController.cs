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
        Wander, Follow, Wait
    }

    [SerializeField] private State currentState;

    void Start()
    {
        var petFollowState = new PetFollowState(agent, animator, this);
        petFollowState.Target = FindObjectOfType<PlayerData>().transform;
        stateMap.Add(State.Follow, petFollowState);

        var petWanderState = new PetWanderState(agent, animator, this);
        stateMap.Add(State.Wander, petWanderState);

        var petWaitState = new PetWaitState(agent, animator, this);
        stateMap.Add(State.Wait, petWaitState);

        SetState(State.Wander);
    }

    void Update()
    {
        GetState(currentState).UpdateState();
    }

    private void LateUpdate()
    {
        GetState(currentState).LateUpdateState();
    }

    public void ChooseRandomState()
    {
        var newState = (State)Random.Range(0, System.Enum.GetValues(typeof(State)).Length);
        //print($"new random state: {newState}");
        SetState(newState);
    }

    public void SetState(State newState)
    {
        GetState(currentState).ExitState();

        currentState = newState;

        GetState(newState).EnterState();
    }

    public void SetFollowTarget()
    {
        SetState(State.Follow);
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
}
