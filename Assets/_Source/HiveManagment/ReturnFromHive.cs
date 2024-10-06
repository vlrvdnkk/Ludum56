using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnFromHive : MonoBehaviour
{
    [SerializeField] private HiveController hiveController;
    [SerializeField] private string mainSceneName;

    public void CompleteHiveAndReturn()
    {
        hiveController.CompleteHive();

        SceneManager.LoadScene(mainSceneName);
    }
}