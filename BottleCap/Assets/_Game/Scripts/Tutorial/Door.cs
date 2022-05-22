using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openRot;
    [SerializeField] private Vector3 closeRot;
    [SerializeField] private float rotateSpeed;

    //TODO sound

    private Vector3 targetRot;

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(
            Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(targetRot), 
            rotateSpeed * Time.deltaTime);
    }

    public void Open()
    {
        targetRot = openRot;
    }

    public void Close()
    {
        targetRot = closeRot;
    }

    public void CloseInstant()
    {
        transform.localRotation = Quaternion.Euler(closeRot);
    }
}
