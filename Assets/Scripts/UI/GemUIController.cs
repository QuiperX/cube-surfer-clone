using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemUIController : MonoBehaviour
{
    //Get textmesh pro object
    
    public TextMeshProUGUI textMeshProUGUI;

    private int gemCount;

    //save gem count in player prefs


    //singleton

    #region Singleton

    public static GemUIController Instance;

    private void Awake()
    {
        gemCount = PlayerPrefs.GetInt("GemCount");
        UpdateText();
        Singleton();
        
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    #endregion

    //get ve set gem count

    public void loadGemCount(int gemCount)
    {
        this.gemCount = gemCount;
    }

    public void printCount()
    {
        Debug.Log("PRINT GEMMMM" + gemCount);
    }

    public int getGemCount()
    {
        return gemCount;
    }
    


    public int IncreaseGemCount(int currentGemCount)
    {
        gemCount = currentGemCount+1;
        PlayerPrefs.SetInt("GemCount", gemCount);
        return gemCount;
    }

    public void UpdateText()
    {
        
        textMeshProUGUI.text = gemCount.ToString();
    }

}
