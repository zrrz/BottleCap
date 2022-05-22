using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceChildAwakeRefresh : MonoBehaviour
{
    void Awake()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.SetActive(false);
        }
    }
}
