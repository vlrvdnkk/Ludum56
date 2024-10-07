using UnityEngine;
using TMPro;

public class DragController : MonoBehaviour
{
    [SerializeField] private Transform startPoint;       // Точка старта
    [SerializeField] private Transform[] pathPoints;     // Массив контрольных точек
    [SerializeField] private Transform endPoint;         // Конечная точка пути
    [SerializeField] private float allowedDistance = 0.5f; // Допустимое расстояние отклонения
    [SerializeField] private GameObject completionText; // Текст завершения игры
    [SerializeField] private GameObject exitButton;   // Кнопка выхода
    [SerializeField] private GameObject Bee44;
    [SerializeField] private AudioSource SmokeSound;
    [SerializeField] private AudioSource Wow;

    private bool isDragging = false;                     // Флаг, указывающий на перетаскивание объекта
    private Vector3 startPosition;                       // Начальная позиция объекта
    private int currentPointIndex = 0;                   // Индекс текущей контрольной точки
    private bool isOutOfBounds = false;                  // Флаг отклонения от пути

    private void Start()
    {
        // Устанавливаем стартовую позицию и начальные состояния UI
        startPosition = startPoint.position;
        transform.position = startPosition;
        completionText.gameObject.SetActive(false);
        Bee44.gameObject.SetActive(false);
        exitButton.SetActive(false);
    }

    private void Update()
    {
        // Начинаем перетаскивание, если нажата кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        // Останавливаем перетаскивание при отпускании кнопки
        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (isDragging)
        {
            DragObject(); // Перетаскиваем объект за мышью

            // Проверяем текущее расстояние до контрольной точки
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) > allowedDistance)
            {
                ResetPosition(); // Если отклонился от траектории, сбрасываем на старт
                return;
            }

            // Если достигли контрольной точки, продвигаемся к следующей
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) <= allowedDistance / 2)
            {
                currentPointIndex++;
                SmokeSound.Play();

                // Если достигли последней точки, завершаем игру
                if (currentPointIndex >= pathPoints.Length)
                {
                    CompleteGame();
                }
            }
        }
    }

    private void StartDragging()
    {
        // Проверяем, наведена ли мышь на объект
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null && collider.OverlapPoint(mousePosition))
        {
            isDragging = true;
            isOutOfBounds = false;
            
        }
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    private void DragObject()
    {
        // Следим за мышью
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void ResetPosition()
    {
        // Сбрасываем на старт и останавливаем перетаскивание
        transform.position = startPosition;
        currentPointIndex = 0;
        isDragging = false;
        isOutOfBounds = true;
    }

    private void CompleteGame()
    {
        isDragging = false;  // Останавливаем перетаскивание
        completionText.gameObject.SetActive(true);  // Показываем текст завершения
        Bee44.gameObject.SetActive(true);  // Показываем текст завершения
        exitButton.SetActive(true);  // Показываем кнопку выхода
        Wow.Play();
    }

    private void OnDrawGizmos()
    {
        // Рисуем линии между контрольными точками для визуализации пути
        Gizmos.color = Color.green;

        if (pathPoints.Length > 0)
        {
            Gizmos.DrawLine(startPoint.position, pathPoints[0].position);
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
            }
            Gizmos.DrawLine(pathPoints[pathPoints.Length - 1].position, endPoint.position);
        }
    }
}
