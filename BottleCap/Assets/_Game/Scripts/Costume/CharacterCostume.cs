using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCostume : MonoBehaviour
{
    public Animator animator;
    public GameObject bottle;

    private void Start()
    {
        bottle.SetActive(false);
    }
}
