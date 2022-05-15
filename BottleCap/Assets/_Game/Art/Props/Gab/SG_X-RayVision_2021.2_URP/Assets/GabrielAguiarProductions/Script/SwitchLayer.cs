using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    public LayerMask defaultLayer;
    public LayerMask xRayLayer;

    private bool xRayActive;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(xRayActive)
            {
                xRayActive = !xRayActive;
                int layerNum = (int)Mathf.Log(defaultLayer.value, 2);
                gameObject.layer = layerNum;

                if (transform.childCount > 0)
                    SetLayerAllChildren(transform, layerNum);
            }
            else
            {
                xRayActive = !xRayActive;
                int layerNum = (int)Mathf.Log(xRayLayer.value, 2);
                gameObject.layer = layerNum;

                if (transform.childCount > 0)
                    SetLayerAllChildren(transform, layerNum);
            }
            
        }
    }

    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }
}
