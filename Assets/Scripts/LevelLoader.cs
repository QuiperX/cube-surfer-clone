using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Player prefs �zerindeki bilgilerle do�ru leveli y�kleriz

    //Start fonksiyonunda prefleri kontrol ederiz.
    

    private void Start()
    {
        
        SceneManager.LoadScene("Level " + (PlayerPrefs.GetInt("LevelIndex") + 1).ToString());

    }
}
