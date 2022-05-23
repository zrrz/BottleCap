using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button selectCharacterButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button quitButton;
    
    [SerializeField] private RectTransform mainMenuPage;
    [SerializeField] private RectTransform characterSelectPage;
 

    [SerializeField] TMPro.TMP_InputField nameInputField;

    void Start()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;

        selectCharacterButton.onClick.AddListener(() =>
        {
            mainMenuPage.gameObject.SetActive(false);
            characterSelectPage.gameObject.SetActive(true);
        });

        cancelButton.onClick.AddListener(() =>
        {
            mainMenuPage.gameObject.SetActive(true);
            characterSelectPage.gameObject.SetActive(false);
        });

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

        if(UserManager.HasUsernameSaved)
        {
            nameInputField.text = UserManager.GetUserName();
        }
        nameInputField.Select();
        nameInputField.ActivateInputField();
    }
}
