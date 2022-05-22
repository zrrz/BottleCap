using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button hideButton;
    [SerializeField] private TMPro.TextMeshProUGUI objectiveTMP;
    [SerializeField] private RectTransform listTransform;

    private static ObjectiveUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(ObjectiveUI)} already exists");
        }

        hideButton.onClick.AddListener(ToggleHide);
        SetObjective("");
    }

    private void ToggleHide()
    {
        listTransform.gameObject.SetActive(!listTransform.gameObject.activeSelf);
    }

    public static void SetObjective(string objectiveText)
    {
        if(objectiveText != "")
        {
            instance.gameObject.SetActive(true);
            instance.listTransform.gameObject.SetActive(true);
            instance.objectiveTMP.text = objectiveText;
        }
        else
        {
            instance.gameObject.SetActive(false);
        }
    }
}
