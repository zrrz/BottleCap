using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    public Button Resumebutton;
    public Button Quitbutton;

    void Start()
    {
        Button btn = Resumebutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickResume);

        Button quitbtn = Quitbutton.GetComponent<Button>();
        quitbtn.onClick.AddListener(TaskOnClickQuit);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void TaskOnClickResume()
    {
        Menu.SetActive(!Menu.activeSelf);
        Debug.Log("Reset");
    }

    //void TaskOnClickReset()
    //{
    //    CharacterControl.JumpCounter.text = "0";
    //    Debug.Log("Reset");
    //}

    void TaskOnClickQuit()
    {
        Application.Quit();
        Debug.Log("Reset");
    }
}
