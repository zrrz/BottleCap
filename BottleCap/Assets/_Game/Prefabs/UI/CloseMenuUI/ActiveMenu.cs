using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public ButtonText Menu;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Menu.gameObject.activeSelf)
            {
                Menu.Open();
            }
            else
            {
                Menu.TaskOnClickResume();
            }
        }
    }
}
