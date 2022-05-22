using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private static WorldManager instance;

    [SerializeField] private float radius = 50f;
    [SerializeField] private Transform dropOffPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(WorldManager)} already exists");
        }
    }

    public static Vector3 GetWorldCenter()
    {
        return instance.transform.position;
    }

    public static float GetWorldRadius()
    {
        return instance.radius;
    }

    public static Transform GetBottleDropOffPoint()
    {
        return instance.dropOffPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
