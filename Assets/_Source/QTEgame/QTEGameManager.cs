using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QTEGameManager : MonoBehaviour
{
    public static QTEGameManager Instance;

    [SerializeField] private HoneycombController[] hives;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private AudioClip keyPressSound;
    [SerializeField] private AudioClip WinSound;
    private AudioSource audioSource;
    private int completedHives = 0;
    private bool gameOver = false;
    private char lastLetter;
    private List<int> hiveIndices;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        winText.gameObject.SetActive(false);
        exitButton.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        InitializeGame();
    }

    private void InitializeGame()
    {
        completedHives = 0;
        gameOver = false;
        InitializeHiveIndices();
        ActivateNextHive();
    }

    private void InitializeHiveIndices()
    {
        hiveIndices = new List<int>();

        for (int i = 0; i < hives.Length; i++)
        {
            hiveIndices.Add(i);
        }
    }

    private void PlayKeyPressSound()
    {
        if (keyPressSound != null)
        {
            audioSource.PlayOneShot(keyPressSound);
        }
    }

    private void PlayWinSound()
    {
        if (WinSound != null)
        {
            audioSource.PlayOneShot(WinSound);
        }
    }



    private void ActivateNextHive()
    {
        if (hiveIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, hiveIndices.Count);
            int hiveIndex = hiveIndices[randomIndex];

            hiveIndices.RemoveAt(randomIndex);

            char letter = GetRandomLetter();
            hives[hiveIndex].ActivateHive(letter);
            PlayKeyPressSound(); // ¬оспроизведение звука при активации нового уль€
        }
        else
        {
            EndGame();
        }
    }

    private char GetRandomLetter()
    {
        char[] letters = { 'Q', 'W', 'E' };
        char newLetter;

        do
        {
            newLetter = letters[Random.Range(0, letters.Length)];
        }
        while (newLetter == lastLetter);

        lastLetter = newLetter;
        return newLetter;
    }

    public void HiveCompleted()
    {
        completedHives++;

        if (completedHives >= hives.Length)
        {
            EndGame();
        }
        else
        {
            ActivateNextHive();
        }
    }

    private void EndGame()
    {
        gameOver = true;
        winText.gameObject.SetActive(true);
        exitButton.SetActive(true);
        PlayWinSound(); // ¬оспроизведение звука при выводе победного текста
    }

    public void RestartGame()
    {
        if (!gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}