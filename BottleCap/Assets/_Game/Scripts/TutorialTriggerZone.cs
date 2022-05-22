using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerZone : MonoBehaviour
{
    [SerializeField] private TutorialManager.TutorialSection endSection;
    //[SerializeField] private TutorialManager.TutorialSection startSection;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TutorialManager.Instance.TriggerEventCompleted(endSection);
            //TutorialManager.Instance.TriggerMomentStart(startSection);
        }
    }
}
