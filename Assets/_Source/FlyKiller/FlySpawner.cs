using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flyPrefab; // Префаб мухи
    [SerializeField] private int flyCount = 10;    // Количество мух
    [SerializeField] private float minX = -8f;     // Минимальная позиция по X для спавна
    [SerializeField] private float maxX = 8f;      // Максимальная позиция по X для спавна
    [SerializeField] private float minY = -4f;     // Минимальная позиция по Y для спавна
    [SerializeField] private float maxY = 4f;      // Максимальная позиция по Y для спавна

    private void Start()
    {
        SpawnFlies();
    }

    private void SpawnFlies()
    {
        for (int i = 0; i < flyCount; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(flyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
