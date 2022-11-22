
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public SwerveMovement SwerveMovement;
    public SwerveInputSystem SwerveInputSystem;
    public PlayerMovement playerMovement;

    public RectTransform winUI;
    public RectTransform loseUI;

    public RectTransform MenuUI;

    
    private int levelIndex;

    private int levelCount;

    public GemUIController gemUIController;

    public TextMeshProUGUI levelCounterText;

    private int levelCounter = 1;

    
    

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        

        levelCounterText.text = "Level " + PlayerPrefs.GetInt("LevelCounter", levelCounter).ToString();

        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
        levelCount = SceneManager.sceneCountInBuildSettings;
        
    }
    #endregion
    
    public void loadGame()
    {
        SceneManager.LoadScene("Level " + (levelIndex + 1).ToString());
    }
    
    public void saveGame()
    {
        
        string activeScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LevelSaved", activeScene);
    }

    public void ActivateWinUI()
    {
        playerMovement.AccessEndPoint();
        PlayerBehaviour.Instance.VictoryAnimation();
        winUI.gameObject.SetActive(true);
        
        
        Vector3 defaultScaleWin = winUI.transform.localScale;

        winUI.transform.localScale = Vector3.one * 0.01f;
        //Debug.Log("defaultScaleWin: " + defaultScaleWin);
        winUI.transform.DOScale(defaultScaleWin, 1f);

    }
    
    public void ActivateLoseUI()
    {

        
        playerMovement.Fail();
        loseUI.gameObject.SetActive(true);
        Vector3 defaultScale = loseUI.transform.localScale;
        loseUI.transform.localScale = Vector3.one * 0.01f;
        loseUI.transform.DOScale(defaultScale, 1f);

    }


    public void startGame()
    {
        playerMovement.StartMotion();
        DeactiveMenuUI();

    }

    public void DeactiveMenuUI()
    {
        MenuUI.gameObject.SetActive(false);
    }

    public void ActivateMenuUI()
    {
        MenuUI.gameObject.SetActive(true);
    }

    public void deactivateWinUI()
    {
        winUI.gameObject.SetActive(false);
    }

    public void Next()
    {

        levelIndex = PlayerPrefs.GetInt("LevelIndex");

        levelCounter = PlayerPrefs.GetInt("LevelCounter");
        if (levelCounter == 0)
        {
            levelCounter = 1;
        }
        
        if (levelIndex < levelCount-2)
        {
            levelIndex++;
        }
        else
        {
            levelIndex = 0;
        }
        levelCounter++;

        SceneManager.LoadScene("Level " + (levelIndex + 1).ToString());
        PlayerPrefs.SetInt("LevelIndex", levelIndex);
        PlayerPrefs.SetInt("LevelCounter", levelCounter);
        

        saveGame();


    }

    public void Restart()
    {
        //load gemCount and update UI

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        int gemCount = PlayerPrefs.GetInt("GemCount");

        gemUIController.loadGemCount(gemCount);
        gemUIController.UpdateText();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
