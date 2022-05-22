using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] TMPro.TMP_InputField nameInputField;

    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            string name = nameInputField.text;
            if (name == "")
            {
                name = "Anonymous";
            }
            UserManager.SetUserName(name);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        nameInputField.Select();
        nameInputField.ActivateInputField();
    }

    void Update()
    {
        
    }
}
