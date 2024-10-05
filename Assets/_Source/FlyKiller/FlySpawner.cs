using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flyPrefab; // ������ ����
    [SerializeField] private int flyCount = 10;    // ���������� ���
    [SerializeField] private float minX = -8f;     // ����������� ������� �� X ��� ������
    [SerializeField] private float maxX = 8f;      // ������������ ������� �� X ��� ������
    [SerializeField] private float minY = -4f;     // ����������� ������� �� Y ��� ������
    [SerializeField] private float maxY = 4f;      // ������������ ������� �� Y ��� ������

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
