using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSign : MonoBehaviour
{

    public GameObject sign;
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
        if (other.tag == "Player")
        {
            sign.SetActive(true);
            Debug.Log("worksign");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sign.SetActive(false);
        }
    }
}

