using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBottle : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float timeToTouchWater = 6f;

    private float timer = 0f;
    private bool waterTouched = false;
    public bool WaterTouched => waterTouched;
    private Vector3 floatDirection;
    private float floatSpeed;

    [SerializeField] private AnimationCurve floatSinkCurve;

    private bool dogCalled = false;

    public void ThrowBottle(Vector3 velocity)
    {
        rb.velocity = velocity;
        timer = timeToTouchWater;

        StartCoroutine(SinkAndCleanup(120f));
    }

    private void Update()
    {
        if(!waterTouched)
        {
            if (!dogCalled)
            {
                timer -= Time.deltaTime;
                if(timer <= 0f)
                {
                    var dog = FindObjectOfType<PetAIController>();
                    dog.SetBottleTarget(this);               
                    dogCalled = true;
                }
            }
        }
        else
        {
            transform.position += floatDirection * floatSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("trigger: " + other.gameObject);
            HandleTouchedWater();
        }
    }

    public void HandleTouchedWater(bool sink = true)
    {
        waterTouched = true;
        Vector3 direction = transform.position - WorldManager.GetWorldCenter();
        direction.y = 0f;
        direction.Normalize();
        floatDirection = direction;
        floatSpeed = Mathf.Max(1f, rb.velocity.magnitude/8f);
        GetComponentInChildren<Animation>().enabled = true;
        StartCoroutine(PlaySinkAndFloat(sink));
        StartCoroutine(Cleanup(25f));
        Destroy(rb);
    }

    private IEnumerator PlaySinkAndFloat(bool sink)
    {
        float yStart = transform.position.y;
        Vector3 rot = transform.eulerAngles;
        Vector3 targetRot = Vector3.zero;
        float animSpeed = 2.8f;
        for(float t = 0; t < 1f; t+= Time.deltaTime/animSpeed)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.up), 30f * Time.deltaTime);
            //transform.eulerAngles = Vector3.Lerp(rot, targetRot, t);
            if(sink)
            {
                Vector3 position = transform.position;
                position.y = yStart + floatSinkCurve.Evaluate(t);
                transform.position = position;
            }
            yield return null;
        }
    }

    private IEnumerator Cleanup(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator SinkAndCleanup(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(rb);

        float speed = 0.3f;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            print("sinking");
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        StartCoroutine(Cleanup(0f));
    }
}
