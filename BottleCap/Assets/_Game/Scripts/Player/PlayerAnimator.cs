using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bottle;

    public Transform HoldingBottleTransform => bottle.transform;

    private void Start()
    {
        SetHoldingBottle(false);   
    }

    public void PlayPickup()
    {
        animator.SetTrigger("PickupBottle");
    }

    public void PlayThrow()
    {
        animator.SetTrigger("ThrowBottle");
    }

    public void SetHoldingBottle(bool holding)
    {
        bottle.SetActive(holding);
        animator.SetLayerWeight(animator.GetLayerIndex("HoldBottleLayer"), holding ? 1f : 0f);
    }
}
