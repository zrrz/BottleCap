using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerAnimator PlayerAnimator { get; private set; }
    public PlayerPickup PlayerPickup { get; private set; }

    public static PlayerData Instance;

    private AnswerDto heldMessage = null;

    public bool IsHoldingMessage => heldMessage != null;

    private void Awake()
    {
        PlayerAnimator = GetComponent<PlayerAnimator>();
        PlayerPickup = GetComponent<PlayerPickup>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(PlayerData)} already exists");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResetInputLock();
        }

        if(IsHoldingMessage)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                PlayerAnimator.PlayThrow();
                AnswerService.SubmitAnswer(heldMessage);
                SetHeldMessage(null);
            }
        }
    }

    public static bool InputLocked => inputLockedCount > 0;

    private static int inputLockedCount = 0;

    public static void AddInputLock()
    {
        inputLockedCount++;
    }

    public static void ReleaseInputLock()
    {
        inputLockedCount = Mathf.Max(0, inputLockedCount - 1);
    }

    public static void ResetInputLock()
    {
        inputLockedCount = 0;
    }

    public void SetHeldMessage(AnswerDto heldMessage)
    {
        this.heldMessage = heldMessage;
        PlayerAnimator.SetHoldingBottle(heldMessage != null);
    }
}
