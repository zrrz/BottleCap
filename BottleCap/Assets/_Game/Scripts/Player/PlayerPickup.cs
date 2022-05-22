using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerData playerData;

    [SerializeField] private float pickupRadius = 2f;
    [SerializeField] private GameObject sign;
    //[SerializeField] private GameObject signQ;

    [SerializeField] private LayerMask interactLayer;

    private int nearbyInteractables = 0;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        if(!playerData.PlayerBottleHolder.IsHoldingMessage)
        {
            InteractNearby();     
        }
        else
        {
            if (sign.activeSelf)
            {
                sign.SetActive(false);
            }
        }
    }

    private void InteractNearby()
    {
        Vector3 center = transform.position + transform.forward;
        Collider[] cols = Physics.OverlapSphere(center, pickupRadius, interactLayer);

        bool interactableNear = false;
        foreach (Collider col in cols)
        {
            Interactable interactable = col.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.F) && !PlayerData.InputLocked)
                {
                    interactable.Interact(playerData);
                }
                interactableNear = true;
                ShowInteractSign(interactable);
                break;
            }
        }
        if(sign.activeSelf && !interactableNear)
        {
            sign.SetActive(false);
        }
    }

    private void ShowInteractSign(Interactable interactable)
    {
        string interactText = "To interact!";
        switch (interactable)
        {
            case MovingBottle movingBottle:
                interactText = "To Pick up";
                break;
            case AnswerWritingTable table:
                interactText = "To Write about yourself";
                break;
            default:
                break;
        }
        sign.SetActive(true);
        sign.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = interactText;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position + transform.forward;
        Gizmos.DrawWireSphere(center, pickupRadius);
    }
}
