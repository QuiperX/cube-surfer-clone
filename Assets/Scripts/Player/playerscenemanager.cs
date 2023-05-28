using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscenemanager : MonoBehaviour
{
    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static playerscenemanager Instance;

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion
}
