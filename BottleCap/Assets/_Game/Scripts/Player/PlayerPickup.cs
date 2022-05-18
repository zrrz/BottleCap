using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerData playerData;

    [SerializeField] private float pickupRadius = 2f;
    public GameObject sign;
    public GameObject signQ;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        if (!PlayerData.InputLocked && Input.GetKeyDown(KeyCode.F))
        {
            InteractNearby();
        }
    }

    private void InteractNearby()
    {
        Vector3 center = transform.position + transform.forward;
        Collider[] cols = Physics.OverlapSphere(center, pickupRadius);
        foreach(Collider col in cols)
        {
            Interactable interactable = col.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(playerData);
                sign.SetActive(false);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position + transform.forward;
        Gizmos.DrawWireSphere(center, pickupRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottle")
        {
            sign.SetActive(true);
        }
        if (other.tag == "Desk")
        {
            signQ.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bottle")
        {
            sign.SetActive(false);
        }
        if (other.tag == "Desk")
        {
            signQ.SetActive(false);
        }
    }
}
