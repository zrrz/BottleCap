using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour
{
    public GameObject Menu;
    public Button Resumebutton;
    public Button Restartbutton;
    public Button Quitbutton;

    void Start()
    {
        Resumebutton.onClick.AddListener(TaskOnClickResume);
        Restartbutton.onClick.AddListener(TaskOnClickRestart);
        Quitbutton.onClick.AddListener(TaskOnClickQuit);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        PlayerData.AddInputLock();
    }    

    public void TaskOnClickResume()
    {
        PlayerData.ReleaseInputLock();
        Menu.SetActive(false);
    }

    public void TaskOnClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PlayerData.ResetInputLock();
    }

    public void TaskOnClickQuit()
    {
        Application.Quit();
    }
}
