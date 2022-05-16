using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 2f;
    public GameObject sign;
    public AnswerUi anwserui;
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GrabNearbyItems();
        }
    }

    private void GrabNearbyItems()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach(Collider col in cols)
        {
            if(col.GetComponent<MovingBottle>())
            {
                Destroy(col.gameObject);
                string answer = col.GetComponent<MovingBottle>().answer.answer;
                anwserui.SetText(answer);
                sign.SetActive(false);
                anwserui.gameObject.SetActive(true);
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
