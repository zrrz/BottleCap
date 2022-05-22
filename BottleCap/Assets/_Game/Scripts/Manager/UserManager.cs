using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static string usernameKey = "USERNAME";

    public static bool HasUsernameSaved => PlayerPrefs.HasKey(usernameKey);

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
}
