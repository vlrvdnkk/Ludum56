using UnityEngine;

public class DelayedMusicStart : MonoBehaviour
{
    public AudioSource musicSource; // ��������� ���� ���� AudioSource ����� ���������

    void Start()
    {
        musicSource.PlayDelayed(21.5f); // �������� �� 22.5 �������
    }
}
    