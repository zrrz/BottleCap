using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public Transform endPosition;

    public MovingBottle bottlePrefab;

    public int spawnCount = 10;
    public float spawnDelay = 1f;

    private float timer = 0f;
    private float numSpawned = 0;

    private bool spawning = true;

    private void Update()
    {
        if (spawning && numSpawned < spawnCount)
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay)
            {
                timer = 0f;
                numSpawned++;
                SpawnPrefab();
            }
        }
    }

    public void SpawnPrefab()
    {
        var bottle = Instantiate(bottlePrefab, transform.position, transform.rotation);
        bottle.SetTargetPosition(endPosition.position);
    }

    private void OnDrawGizmos()
    {
        if(!endPosition)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.DrawWireSphere(endPosition.position, 1f);
        Gizmos.DrawLine(transform.position, endPosition.position);
    }
}
