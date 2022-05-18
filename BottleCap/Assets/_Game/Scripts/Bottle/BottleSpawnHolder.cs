using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawnHolder : MonoBehaviour
{
    [SerializeField] public Transform endPosition;

    private void OnValidate()
    {
        if(endPosition == null)
        {
            endPosition = transform.GetChild(0);
        }
    }

    private void OnDrawGizmos()
    {
        if (!endPosition)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.DrawWireSphere(endPosition.position, 1f);
        Gizmos.DrawLine(transform.position, endPosition.position);
    }
}
