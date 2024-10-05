using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;           // Скорость движения мухи
    [SerializeField] private GameObject bloodStainPrefab;    // Префаб пятна крови

    private Vector2 targetPosition;                          // Цель движения

    private void Start()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        MoveFly();
    }

    private void MoveFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Если муха достигла цели, задаём новую случайную позицию
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-8f, 8f);   // В зависимости от размера экрана, можно изменить диапазон
        float randomY = Random.Range(-4f, 4f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnMouseDown()
    {
        KillFly();
    }

    private void KillFly()
    {
        // Спавним пятно крови на месте мухи
        Instantiate(bloodStainPrefab, transform.position, Quaternion.identity);

        // Уведомляем GameManager об уничтожении мухи
        FlyKillerGameManager.Instance.FlyKilled();

        // Уничтожаем муху
        Destroy(gameObject);
    }
}
