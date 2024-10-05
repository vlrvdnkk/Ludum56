using TMPro;
using UnityEngine;

public class FlyKillerGameManager : MonoBehaviour
{
    public static FlyKillerGameManager Instance;  // Singleton для удобного доступа
    [SerializeField] private int totalFlies = 10;   // Общее количество мух
    [SerializeField] private TextMeshProUGUI gameOverText;  // Текст для сообщения об окончании игры
    [SerializeField] private GameObject exitButton;

    private int fliesKilled = 0;  // Количество убитых мух

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);  // Скрываем текст в начале игры
        exitButton.gameObject.SetActive(false);    // Скрываем кнопку выхода в начале игры
    }

    // Метод для уведомления об убийстве мухи
    public void FlyKilled()
    {
        fliesKilled++;

        // Если убиты все мухи, завершаем игру
        if (fliesKilled >= totalFlies)
        {
            EndGame();
        }
    }

    // Завершение игры: активируем текст и кнопку
    private void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);    // Показываем кнопку выхода
    }
}
