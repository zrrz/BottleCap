using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawnManager : MonoBehaviour
{
    [SerializeField] private MovingBottle bottlePrefab;
    [SerializeField] private BottleSpawnHolder[] bottleSpawns;

    [SerializeField]  private float minBottleSpawnFrequency = 2f;
    [SerializeField]  private float maxBottleSpawnFrequency = 10f;

    private List<BottleSpawnHolder> availableBottleSpawns;
    private float timer;

    private void OnValidate()
    {
        bottleSpawns = GetComponentsInChildren<BottleSpawnHolder>();
    }

    private void Start()
    {
        availableBottleSpawns = new List<BottleSpawnHolder>(bottleSpawns);
    }

    void Update()
    {
        if (AnswerService.Ready == false)
        {
            Debug.Log("not ready");
            return;
        }

        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            AnswerDto newAnswer = AnswerService.GetNewAnswer();
            if (newAnswer != null && availableBottleSpawns.Count > 0)
            {
                SpawnBottle(newAnswer);
            }
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = Random.Range(minBottleSpawnFrequency, maxBottleSpawnFrequency);
    }

    public void SpawnBottle(AnswerDto newAnswer)
    {
        int randIndex = Random.Range(0, availableBottleSpawns.Count);
        var bottleSpawn = availableBottleSpawns[randIndex];
        Vector3 endPosition = bottleSpawn.endPosition.position;
        availableBottleSpawns.RemoveAt(randIndex);
        var bottle = Instantiate(bottlePrefab, bottleSpawn.transform.position, bottleSpawn.transform.rotation);
        bottle.Initialize(endPosition, newAnswer, ()=>
        {
            availableBottleSpawns.Add(bottleSpawn);
        });
    }
}
