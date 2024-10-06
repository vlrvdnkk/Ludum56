using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiveController : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string hiveKey;
    [SerializeField] private bool isFinalHive = false;
    [SerializeField] private string[] requiredHives;
    [SerializeField] private LayerMask playerLayer;

    private bool isPlayerInTrigger = false;
    private bool isCompleted = false;

    private void Start()
    {
        isCompleted = PlayerPrefs.GetInt(hiveKey, 0) == 1;
        interactionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (IsPlayerLayer(other.gameObject) && !isCompleted)
        {
            if (isFinalHive && !CheckRequiredHives())
            {
                interactionText.text = "You must complete all other hives first!";
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.text = "Press E to enter";
                interactionText.gameObject.SetActive(true);
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayerLayer(other.gameObject))
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isFinalHive && CheckRequiredHives())
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
        if (!isCompleted)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void CompleteHive()
    {
        isCompleted = true;
        PlayerPrefs.SetInt(hiveKey, 1);
        PlayerPrefs.Save();

        Collider hiveCollider = GetComponent<Collider>();
        if (hiveCollider != null)
        {
            hiveCollider.enabled = false;
        }
    }

    private bool CheckRequiredHives()
    {
        foreach (string requiredHiveKey in requiredHives)
        {
            if (PlayerPrefs.GetInt(requiredHiveKey, 0) != 1)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsPlayerLayer(GameObject obj)
    {
        return (playerLayer.value & (1 << obj.layer)) > 0;
    }
}