using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 2f;
    public GameObject sign;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GrabNearbyItems();
        }
    }

    private void GrabNearbyItems()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach(Collider col in cols)
        {
            MovingBottle bottle = col.GetComponent<MovingBottle>();
            if (bottle != null)
            {
                Destroy(col.gameObject);
                string answerText = bottle.answerData.answer;
                string promptText = bottle.answerData.prompt;
                AnswerUI.Instance.SetText(promptText, answerText);
                sign.SetActive(false);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bottle")
        {
            sign.SetActive(true);
            Debug.Log("worksign");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bottle")
        {
            sign.SetActive(false);
        }
    }
}
