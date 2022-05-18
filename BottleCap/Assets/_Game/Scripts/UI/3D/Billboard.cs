using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 dir = transform.position - Camera.main.transform.position;

        transform.rotation = Quaternion.LookRotation(dir.normalized);// (Camera.main.transform.position);
    }
}
