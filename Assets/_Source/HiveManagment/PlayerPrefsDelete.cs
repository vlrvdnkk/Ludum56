using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDelete : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
