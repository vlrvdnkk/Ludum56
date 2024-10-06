using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiveController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string hiveKey;
    [SerializeField] private bool isFinalHive = false;
    [SerializeField] private string[] requiredHives;
    [SerializeField] private LayerMask playerLayer;

    private bool isPlayerInTrigger = false;
    private bool isCompleted = false;

    private void Start()
    {
        // ���������, ��� �� ���� ���� ��� �����������
        isCompleted = PlayerPrefs.GetInt(hiveKey, 0) == 1;

        // ��������� ���������� ����, ���� ���� ��� �������
        if (isCompleted)
        {
            interactionText.gameObject.SetActive(false);
            gameObject.SetActive(false);  // ��������� ����, ���� �� ��� ��� �����������
        }
        else
        {
            interactionText.gameObject.SetActive(false);  // �������� ����� "E" � ������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ghjk");
        // ���� ������ ������ � ���������� ���� � ����������� ���� ������
        if (IsPlayerLayer(other.gameObject) && !isCompleted)
        {
            if (isFinalHive && !CheckRequiredHives())  // ���� ��� ����� ���� � �� ��� ���������� ��������
            {
                interactionText.text = "You must complete all other hives first!";
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.text = "Press E to enter";  // ������� ����� "E"
                interactionText.gameObject.SetActive(true);
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� ����� ������� �� ���������� ����
        if (IsPlayerLayer(other.gameObject))
        {
            interactionText.gameObject.SetActive(false);  // �������� ����� "E"
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        // ���������, ������ �� ������ E � ��������� �� ����� � ���������� ����
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isFinalHive && CheckRequiredHives())  // ��������� ������� ��� ������ ����
            {
                LoadHiveScene();
            }
            else if (!isFinalHive)
            {
                LoadHiveScene();
            }
        }
    }

    private void LoadHiveScene()
    {
        // ������� �� �����, ���� ���� ��� �� �������
        if (!isCompleted)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // ���� ����� ����������, ����� ����� ������������ �� ����� ����
    public void CompleteHive()
    {
        isCompleted = true;
        PlayerPrefs.SetInt(hiveKey, 1);  // ��������� �������� � PlayerPrefs
        PlayerPrefs.Save();

        // ��������� ���� � ��� ���������� ����
        interactionText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // ��������, ��� ��� ������ ���� ��������
    private bool CheckRequiredHives()
    {
        foreach (string requiredHiveKey in requiredHives)
        {
            if (PlayerPrefs.GetInt(requiredHiveKey, 0) != 1)
            {
                return false;  // ���� ���� �� ���� ���� �� �������, ���������� false
            }
        }
        return true;  // ��� ���������� ���� ��������
    }

    // ��������, ��������� �� ������ �� ���� ������
    private bool IsPlayerLayer(GameObject obj)
    {
        return (playerLayer.value & (1 << obj.layer)) > 0;
    }
}