using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager Instance;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject player; 
    [SerializeField] private Transform wallSpawnPoint;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;

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
                float randomY = Random.Range(minY, maxY);
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

        player.GetComponent<BirdController>().StopPlayerMovement();
    }

    public bool IsGameActive()
    {
        return gameActive;
    }
}