using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInUI : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image image;

    private static FadeInUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(FadeInUI)} already exists");
        }
    }

    private void Start()
    {
        FadeIn(2.5f);
    }

    public void FadeIn(float fadeInTime)
    {
        StopAllCoroutines();
        StartCoroutine(_FadeIn(fadeInTime));
    }

    private IEnumerator _FadeIn(float fadeInTime)
    {
        image.gameObject.SetActive(true);
        Color startColor = Color.black;
        Color endColor = new Color(0f, 0f, 0f, 0f);
        for(float t = 0; t < 1f; t+= Time.deltaTime/fadeInTime)
        {
            image.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        print("Done");
        image.gameObject.SetActive(false);
    }
}
