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

    void Update()
    {
        float moveSpeed = 0f;
        bool sprinting = false;
        float inputSpeed = 0f;

        if (!PlayerData.InputLocked)
        {
            Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            if(inputDir.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(inputDir), turnSpeed * Time.deltaTime);
            }

            sprinting = Input.GetButton("Sprint");
            moveSpeed = sprinting ? sprintSpeed : walkSpeed;
            inputSpeed = Mathf.Clamp01(inputDir.magnitude);
        }

        characterController.SimpleMove(inputSpeed * transform.forward * moveSpeed);
        animator.SetFloat("MoveSpeed", inputSpeed * (sprinting ? 1f : .5f));
    }
}
