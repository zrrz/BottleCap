using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qwhistle : MonoBehaviour
{

    public AudioSource sound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sound.Play();
        }
    }
}
