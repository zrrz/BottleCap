using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class PetBaseState : FSMState
{
    protected NavMeshAgent navMeshAgent;
    protected PetAIController petAiController;
    protected Vector3 destination;
    protected Animator animator;

    protected float minMoveSpeed = 2.5f;
    protected float maxMoveSpeed = 4f;

    public PetBaseState(NavMeshAgent navMeshAgent, Animator animator, PetAIController petAiController)
    {
        this.navMeshAgent = navMeshAgent;
        this.petAiController = petAiController;
        this.animator = animator;
    }

    public override void UpdateState()
    {
        float animSpeed = Mathf.InverseLerp(minMoveSpeed, maxMoveSpeed, navMeshAgent.speed);
        animator.SetFloat("MoveSpeed", 1f + animSpeed);

        MoveTowardsDestination();
    }

    public override void LateUpdateState()
    {
        //TiltHead();
    }

    //protected void TiltHead()
    //{
    //    Transform head = petAiController.transform.GetChild(0).Find("Armature/Hip1/Spine1/Spine2/Spine3/Neck/Head");
    //    Transform player = GameObject.FindObjectOfType<PlayerData>().transform;

    //    Vector3 direction = player.position + Vector3.up - head.position;
    //    head.LookAt(direction, -head.forward);
    //}

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

        Transform visObject = transform.GetChild(0);

        //Tilt to slope
        if(Physics.Raycast(visObject.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(visObject.position, hit.point, Color.red);

            Quaternion newRot = Quaternion.FromToRotation(visObject.up, hit.normal)
            * visObject.rotation;

            float slopeRotChangeSpeed = 4f;

            //Apply the rotation 
            visObject.rotation = Quaternion.Lerp(visObject.rotation, newRot,
                Time.deltaTime * slopeRotChangeSpeed);

            Vector3 angles = visObject.localEulerAngles;
            angles.y = 0f;
            visObject.localEulerAngles = angles;
        }
    }
    
    protected void SetMoveSpeed(float moveSpeed)
    {
        navMeshAgent.speed = moveSpeed;
    }
}
