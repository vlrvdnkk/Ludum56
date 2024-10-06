using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumeсontroller : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.minValue = 0f; // Устанавливаем минимальное значение слайдера
        volumeSlider.maxValue = 1f; // Устанавливаем максимальное значение слайдера
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Lerp(-80f, 0f, volume);
        audioMixer.SetFloat("MasterVolume", dB);
    }
}
