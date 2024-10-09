using UnityEngine;

public class PlayerPrefsDelete : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
