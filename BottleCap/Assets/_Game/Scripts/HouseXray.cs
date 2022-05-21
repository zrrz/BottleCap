using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseXray : MonoBehaviour
{
    public List<GameObject> roofs;
    public Material xray;
    public Material norm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            foreach(GameObject roof in roofs)
            {
                roof.GetComponent<MeshRenderer>().material = xray;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach(GameObject roof in roofs)
            {
                roof.GetComponent<MeshRenderer>().material = norm;
            }
        }
    }
}
