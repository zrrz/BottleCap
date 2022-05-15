using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseXray : MonoBehaviour
{
    public GameObject roof;
    public GameObject roof1;
    public Material xray;
    public Material norm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            roof.GetComponent<MeshRenderer>().material = xray;
            roof1.GetComponent<MeshRenderer>().material = xray;
            Debug.Log("work");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            roof.GetComponent<MeshRenderer>().material = norm;
            roof1.GetComponent<MeshRenderer>().material = norm;
        }
    }
}
