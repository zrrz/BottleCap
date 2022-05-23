using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteractable : Interactable
{
    [SerializeField] private PetAIController petAIController;

    public override void Interact(PlayerData playerPickup)
    {
        petAIController.InteractWithDog();
    }
}
