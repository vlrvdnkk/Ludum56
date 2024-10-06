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
        // Проверяем, был ли этот улей уже активирован
        isCompleted = PlayerPrefs.GetInt(hiveKey, 0) == 1;

        // Отключаем триггерную зону, если улей уже пройден
        if (isCompleted)
        {
            interactionText.gameObject.SetActive(false);
            gameObject.SetActive(false);  // Отключаем улей, если он уже был активирован
        }
        else
        {
            interactionText.gameObject.SetActive(false);  // Скрываем текст "E" в начале
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ghjk");
        // Если объект входит в триггерную зону и принадлежит слою игрока
        if (IsPlayerLayer(other.gameObject) && !isCompleted)
        {
            if (isFinalHive && !CheckRequiredHives())  // Если это пятый улей и не все предыдущие пройдены
            {
                interactionText.text = "You must complete all other hives first!";
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.text = "Press E to enter";  // Обычный текст "E"
                interactionText.gameObject.SetActive(true);
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Если игрок выходит из триггерной зоны
        if (IsPlayerLayer(other.gameObject))
        {
            interactionText.gameObject.SetActive(false);  // Скрываем текст "E"
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        // Проверяем, нажата ли кнопка E и находится ли игрок в триггерной зоне
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isFinalHive && CheckRequiredHives())  // Проверяем условия для пятого улья
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
        // Переход на сцену, если улей ещё не пройден
        if (!isCompleted)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Этот метод вызывается, когда игрок возвращается из сцены улья
    public void CompleteHive()
    {
        isCompleted = true;
        PlayerPrefs.SetInt(hiveKey, 1);  // Сохраняем прогресс в PlayerPrefs
        PlayerPrefs.Save();

        // Отключаем улей и его триггерную зону
        interactionText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // Проверка, что все нужные ульи пройдены
    private bool CheckRequiredHives()
    {
        foreach (string requiredHiveKey in requiredHives)
        {
            if (PlayerPrefs.GetInt(requiredHiveKey, 0) != 1)
            {
                return false;  // Если хотя бы один улей не пройден, возвращаем false
            }
        }
        return true;  // Все предыдущие ульи пройдены
    }

    // Проверка, находится ли объект на слое игрока
    private bool IsPlayerLayer(GameObject obj)
    {
        return (playerLayer.value & (1 << obj.layer)) > 0;
    }
}