using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static string usernameKey = "USERNAME";
    private static string costumeIndexKey = "COSTUME";

    public static bool HasUsernameSaved => PlayerPrefs.HasKey(usernameKey);
    public static bool HasCostumeIndexSaved => PlayerPrefs.HasKey(costumeIndexKey);

    public static void SetUserName(string name)
    {
        PlayerPrefs.SetString(usernameKey, name);
        PlayerPrefs.Save();
    }

    public static string GetUserName()
    {
        string username = "Anonymoos";
        if (HasUsernameSaved)
        {
            username = PlayerPrefs.GetString(usernameKey);
        }
        return username;
    }

    public static void SetCostumeIndex(int index)
    {
        PlayerPrefs.SetInt(costumeIndexKey, index);
        PlayerPrefs.Save();
    }

    public static int GetCostumeIndex()
    {
        int costumeIndex = 0;
        if (HasCostumeIndexSaved)
        {
            costumeIndex = PlayerPrefs.GetInt(costumeIndexKey);
        }
        return costumeIndex;
    }
}
