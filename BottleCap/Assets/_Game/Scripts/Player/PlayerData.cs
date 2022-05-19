using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerAnimator PlayerAnimator { get; private set; }
    public PlayerPickup PlayerPickup { get; private set; }

    private void Awake()
    {
        PlayerAnimator = GetComponent<PlayerAnimator>();
        PlayerPickup = GetComponent<PlayerPickup>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ResetInputLock();
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
}
