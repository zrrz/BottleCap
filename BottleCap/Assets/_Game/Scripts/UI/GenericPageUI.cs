using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPageUI : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        PlayerData.AddInputLock();
    }

    protected void Close()
    {
        gameObject.SetActive(false);
        PlayerData.ReleaseInputLock();
    }
}
