using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    [SerializeField] private float turnSpeed = 360f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if(inputDir.sqrMagnitude > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(inputDir), turnSpeed * Time.deltaTime);
        }

        bool sprinting = Input.GetButton("Sprint");
        float inputSpeed = Mathf.Clamp01(inputDir.magnitude);
        float moveSpeed = sprinting ? sprintSpeed : walkSpeed;
        characterController.SimpleMove(inputSpeed * transform.forward * moveSpeed);

        animator.SetFloat("MoveSpeed", inputSpeed * (sprinting ? 1f : .5f));
    }
}
