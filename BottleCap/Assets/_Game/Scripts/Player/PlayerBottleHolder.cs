using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBottleHolder : MonoBehaviour
{
    private AnswerDto heldMessage = null;
    public bool IsHoldingMessage => heldMessage != null;

    public PlayerData playerData;

    [SerializeField] ThrowingBottle throwingBottlePrefab;

    public bool throwingLocked = false;

    void Update()
    {
        if (IsHoldingMessage && !throwingLocked)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(ThrowBottleAsync());
            }
        }
    }

    private IEnumerator ThrowBottleAsync()
    {
        var message = heldMessage;
        heldMessage = null;

        playerData.PlayerAnimator.PlayThrow();
        yield return new WaitForSeconds(0.7f);
        AnswerService.SubmitAnswer(message);
        SetHeldMessage(null);
        Vector3 spawnPosition = playerData.PlayerAnimator.HoldingBottleTransform.position;
        Quaternion spawnRotation = playerData.PlayerAnimator.HoldingBottleTransform.rotation;
        var bottle = Instantiate(throwingBottlePrefab, spawnPosition, spawnRotation);
        bottle.ThrowBottle(transform.forward * 6f + transform.up * 5f);
    }

    public void SetHeldMessage(AnswerDto heldMessage)
    {
        this.heldMessage = heldMessage;
        playerData.PlayerAnimator.SetHoldingBottle(heldMessage != null);
    }

    public void LockThrowing()
    {
        throwingLocked = true;
    }

    public void UnlockThrowing()
    {
        throwingLocked = false;
    }
}
