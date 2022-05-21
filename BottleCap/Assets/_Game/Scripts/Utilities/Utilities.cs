using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomCollectionExtension
{
    public static T RandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
    public static T RandomItem<T>(this List<T> array)
    {
        return array[Random.Range(0, array.Count)];
    }
}

public static class Vector3Extension
{
    public static Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t <= 0.5f)
        {
            return Vector3.Lerp(a, b, t * 2f);
        }
        else
        {
            return Vector3.Lerp(b, c, (t * 2f) - 1f);
        }
    }
}

public static class LayerMaskExtension
{
    public static bool IsInLayerMask(this LayerMask mask, int layer)
    {
        return ((mask.value & (1 << layer)) > 0);
    }
}
