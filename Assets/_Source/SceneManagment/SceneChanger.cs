using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int scene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }
}