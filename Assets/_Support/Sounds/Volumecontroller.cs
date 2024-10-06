using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume�ontroller : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.minValue = 0f; // ������������� ����������� �������� ��������
        volumeSlider.maxValue = 1f; // ������������� ������������ �������� ��������
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Lerp(-80f, 0f, volume);
        audioMixer.SetFloat("MasterVolume", dB);
    }
}
