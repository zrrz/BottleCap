using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public Transform Holder;

    public int spawnCount = 10;
    public float spawnDelay = 1f;

    private float timer = 0f;
    private float numSpawned = 0;

    private bool spawning = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        Instantiate(prefab, Holder.position, Holder.rotation);
    }
}
