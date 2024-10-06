using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoneycombController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float timeToPress = 2f;

    private char currentLetter;
    private bool isActive = false;
    private float timer;

    public void ActivateHive(char letter)
    {
        if (!isActive)
        {
            isActive = true;
            currentLetter = letter;
            letterText.text = currentLetter.ToString();
            timer = timeToPress;
            StartCoroutine(TimeCountdown());
        }
    }

    private IEnumerator TimeCountdown()
    {
        timeSlider.maxValue = timeToPress;
        timeSlider.value = timeToPress;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timeSlider.value = timer;

            if (Input.GetKeyDown(currentLetter.ToString().ToLower()))
            {
                Success();
                yield break;
            }

            yield return null;
        }

        Fail();
    }

    private void Success()
    {
        letterText.text = "";
        QTEGameManager.Instance.HiveCompleted();
    }

    private void Fail()
    {
        QTEGameManager.Instance.RestartGame();
    }
}