using UnityEngine;
using TMPro;

public class DragController : MonoBehaviour
{
    [SerializeField] private Transform startPoint;       // ����� ������
    [SerializeField] private Transform[] pathPoints;     // ������ ����������� �����
    [SerializeField] private Transform endPoint;         // �������� ����� ����
    [SerializeField] private float allowedDistance = 0.5f; // ���������� ���������� ����������
    [SerializeField] private GameObject completionText; // ����� ���������� ����
    [SerializeField] private GameObject exitButton;   // ������ ������
    [SerializeField] private GameObject Bee44;
    [SerializeField] private AudioSource SmokeSound;
    [SerializeField] private AudioSource Wow;

    private bool isDragging = false;                     // ����, ����������� �� �������������� �������
    private Vector3 startPosition;                       // ��������� ������� �������
    private int currentPointIndex = 0;                   // ������ ������� ����������� �����
    private bool isOutOfBounds = false;                  // ���� ���������� �� ����

    private void Start()
    {
        // ������������� ��������� ������� � ��������� ��������� UI
        startPosition = startPoint.position;
        transform.position = startPosition;
        completionText.gameObject.SetActive(false);
        Bee44.gameObject.SetActive(false);
        exitButton.SetActive(false);
    }

    private void Update()
    {
        // �������� ��������������, ���� ������ ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        // ������������� �������������� ��� ���������� ������
        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (isDragging)
        {
            DragObject(); // ������������� ������ �� �����

            // ��������� ������� ���������� �� ����������� �����
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) > allowedDistance)
            {
                ResetPosition(); // ���� ���������� �� ����������, ���������� �� �����
                return;
            }

            // ���� �������� ����������� �����, ������������ � ���������
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) <= allowedDistance / 2)
            {
                currentPointIndex++;
                SmokeSound.Play();

                // ���� �������� ��������� �����, ��������� ����
                if (currentPointIndex >= pathPoints.Length)
                {
                    CompleteGame();
                }
            }
        }
    }

    private void StartDragging()
    {
        // ���������, �������� �� ���� �� ������
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
        // ������ �� �����
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void ResetPosition()
    {
        // ���������� �� ����� � ������������� ��������������
        transform.position = startPosition;
        currentPointIndex = 0;
        isDragging = false;
        isOutOfBounds = true;
    }

    private void CompleteGame()
    {
        isDragging = false;  // ������������� ��������������
        completionText.gameObject.SetActive(true);  // ���������� ����� ����������
        Bee44.gameObject.SetActive(true);  // ���������� ����� ����������
        exitButton.SetActive(true);  // ���������� ������ ������
        Wow.Play();
    }

    private void OnDrawGizmos()
    {
        // ������ ����� ����� ������������ ������� ��� ������������ ����
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
