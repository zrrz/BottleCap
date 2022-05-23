using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public CharacterCostume[] characterCostumes;
    private CharacterCostume currentCostume;

    private Animator Animator => currentCostume.animator;
    private GameObject Bottle => currentCostume.bottle;

    public Transform HoldingBottleTransform => Bottle.transform;

    private void Awake()
    {
        LoadCostume();
        SetHoldingBottle(false);
    }

    private void LoadCostume()
    {
        int costumeIndex = UserManager.GetCostumeIndex();
        currentCostume = characterCostumes[costumeIndex];
        SetCostume(costumeIndex);
    }

    public void SetCostume(int index)
    {
        for(int i = 0; i < characterCostumes.Length; i++)
        {
            characterCostumes[i].gameObject.SetActive(i == index);
        }
    }

    public void PlayPickup()
    {
        Animator.SetTrigger("PickupBottle");
    }

    public void PlayThrow()
    {
        Animator.SetTrigger("ThrowBottle");
    }

    public void SetHoldingBottle(bool holding)
    {
        Bottle.SetActive(holding);
        Animator.SetLayerWeight(Animator.GetLayerIndex("HoldBottleLayer"), holding ? 1f : 0f);
    }

    public void SetMoveSpeed(float speed)
    {
        Animator.SetFloat("MoveSpeed", speed);
    }
}
