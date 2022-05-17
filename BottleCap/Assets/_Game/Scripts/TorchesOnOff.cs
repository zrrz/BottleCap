using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchesOnOff : MonoBehaviour
{
    public GameObject torch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            torch.SetActive(!torch.activeSelf);
        }

    }

}
