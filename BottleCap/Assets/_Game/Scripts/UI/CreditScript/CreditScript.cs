using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScript : MonoBehaviour
{
    public Button yourButton;
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void TaskOnClick()
    {
        credits.SetActive(!credits.activeSelf);
    }
}
