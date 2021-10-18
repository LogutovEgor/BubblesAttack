using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bubblePrefabs = default;
    [SerializeField]
    private float spawnDelayInSeconds = default;

    [SerializeField]
    private float innerSpawnAreaRadius = default;
    [SerializeField]
    private float outerSpawnAreaRadius = default;

    private float timer = default;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnBubble();
            ResetTimer();
        }
    }

    private void ResetTimer() => timer = spawnDelayInSeconds;

    private void SpawnBubble()
    {
        GameObject newBubble = Instantiate(GetRandomBubblePrefab());

        float radius = Random.Range(innerSpawnAreaRadius, outerSpawnAreaRadius);
        Vector2 position = Random.insideUnitCircle * radius;

        newBubble.transform.position = position;
    }

    private GameObject GetRandomBubblePrefab()
    {
        int index = Random.Range(0, bubblePrefabs.Length);
        return bubblePrefabs[index];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, innerSpawnAreaRadius);

        Gizmos.DrawSphere(transform.position, outerSpawnAreaRadius);
    }
}
