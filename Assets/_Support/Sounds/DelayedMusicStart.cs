using UnityEngine;

public class DelayedMusicStart : MonoBehaviour
{
    public AudioSource musicSource; // Привяжите сюда свой AudioSource через инспектор

    void Start()
    {
        musicSource.PlayDelayed(21.5f); // Задержка на 22.5 секунды
    }
}
    