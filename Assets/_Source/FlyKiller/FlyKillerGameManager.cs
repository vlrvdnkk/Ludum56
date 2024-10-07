using TMPro;
using UnityEngine;

public class FlyKillerGameManager : MonoBehaviour
{
    public static FlyKillerGameManager Instance;
    [SerializeField] private int totalFlies = 10;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private AudioSource WinSound;
    [SerializeField] private AudioSource FlyKillSound;

    private int fliesKilled = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void FlyKilled()
    {
        fliesKilled++;
        FlyKillSound.Play();

        if (fliesKilled >= totalFlies)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        WinSound.Play(); // Проигрываем звук победы
    }
}
