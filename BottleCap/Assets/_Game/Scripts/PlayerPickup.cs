using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 2f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GrabNearbyItems();
        }
    }

    private void GrabNearbyItems()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach(Collider col in cols)
        {
            if(col.GetComponent<MovingBottle>())
            {
                Destroy(col.gameObject);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
