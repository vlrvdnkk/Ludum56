using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnFromHive : MonoBehaviour
{
    [SerializeField] private int scene;
    [SerializeField] private string hiveKey;
    public void CompleteHive()
    {
        PlayerPrefs.SetInt(hiveKey, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(scene);
    }
}