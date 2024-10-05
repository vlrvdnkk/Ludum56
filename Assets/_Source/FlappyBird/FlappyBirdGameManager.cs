using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager Instance;

    [SerializeField] private GameObject wallPrefab; // Префаб стены
    [SerializeField] private GameObject player; 
    [SerializeField] private Transform wallSpawnPoint; // Точка спавна стен
    [SerializeField] private float spawnInterval = 2f; // Интервал между спавном стен
    [SerializeField] private TextMeshProUGUI gameOverText; // Текст окончания игры
    [SerializeField] private GameObject exitButton; // Кнопка выхода

    [SerializeField] private float minY = -2f; // Минимальное значение Y для спавна стен
    [SerializeField] private float maxY = 2f;  // Максимальное значение Y для спавна стен

    private int wallCount = 0;
    private int maxWalls = 10;
    private bool gameActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        wallCount = 0;
        gameActive = true;
        StartCoroutine(SpawnWalls());
        gameOverText.gameObject.SetActive(false);
        exitButton.SetActive(false);
    }

    private IEnumerator SpawnWalls()
    {
        while (wallCount < maxWalls)
        {
            if (gameActive)
            {
                float randomY = Random.Range(minY, maxY);  // Генерируем случайное значение Y
                Vector3 spawnPosition = new Vector3(wallSpawnPoint.position.x, randomY, wallSpawnPoint.position.z);
                Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
                wallCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        EndGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EndGame()
    {
        gameActive = false;
        gameOverText.text = "You Win!";
        gameOverText.gameObject.SetActive(true);
        exitButton.SetActive(true);

        // Останавливаем движение игрока
        player.GetComponent<BirdController>().StopPlayerMovement();
    }

    public bool IsGameActive()
    {
        return gameActive;
    }
}