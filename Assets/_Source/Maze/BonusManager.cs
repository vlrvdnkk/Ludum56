using UnityEngine;
using TMPro;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance;

    [SerializeField] private int totalBonuses = 5;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private InputListener inputListener;

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

        if (collectedBonuses >= totalBonuses)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        winText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        inputListener.gameObject.SetActive(false);
    }
}
