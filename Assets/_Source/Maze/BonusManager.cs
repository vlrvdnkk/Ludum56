using UnityEngine;
using TMPro;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance;

    [SerializeField] private int totalBonuses = 5;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private InputListener inputListener;
    [SerializeField] private AudioSource ClaimSound;
    [SerializeField] private AudioSource WinSound; // Добавляем AudioSource для победного звука

    private int collectedBonuses = 0;

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
        exitButton.gameObject.SetActive(false);
    }

    public void BonusCollected()
    {
        collectedBonuses++;
        PlayClaimSound();

        if (collectedBonuses >= totalBonuses)
        {
            EndLevel();
        }
    }

    private void PlayClaimSound()
    {
        if (ClaimSound != null)
        {
            ClaimSound.Play();
        }
    }

    private void EndLevel()
    {
        winText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        inputListener.gameObject.SetActive(false);
        PlayWinSound(); // Вызов метода для проигрывания победного звука
    }

    private void PlayWinSound()
    {
        if (WinSound != null)
        {
            WinSound.Play();
        }
    }
}
