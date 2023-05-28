using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
using TMPro;
using System.Collections;

// Add this namespace
using HuaweiMobileServices.Ads;
using HmsPlugin;

public class GameManager : MonoBehaviour
{

    //public SwerveMovement SwerveMovement;
    //public SwerveInputSystem SwerveInputSystem;
    public PlayerMovement playerMovement;


    public RectTransform winUI;
    public RectTransform loseUI;
    public RectTransform ShoppingUI;

    public RectTransform MenuUI;

    private const string GEM_COUNT_KEY = "GemCount";

    private int levelIndex;

    private int levelCount;

    public GemUIController gemUIController;

    public TextMeshProUGUI levelCounterText;

    private int levelCounter = 1;

    //PlayerPrefs.SetInt("removeAds", 0);


    // Reference to the interstitial ad object
    private InterstitialAd interstitialAd;

    

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {

        Debug.Log("GameManager Awake INSIDE");


        levelCounterText.text = "Level " + PlayerPrefs.GetInt("LevelCounter", levelCounter).ToString();

        if (instance != null && instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        instance = this;
        //DontDestroyOnLoad(this);
        levelCount = SceneManager.sceneCountInBuildSettings;

        // Add this code

        if (PlayerPrefs.HasKey("RemoveAds"))
        {
            // Get the value of RemoveAds
            int removeAds = PlayerPrefs.GetInt("RemoveAds");
            // Check if the value is 0 or 1
            if (removeAds == 0)
            {
                //bannerId
                HMSAdsKitManager.Instance.ShowBannerAd();
            }
            else if (removeAds == 1)
            {
                HMSAdsKitManager.Instance.HideBannerAd();
            }
        }
        else
        {
            PlayerPrefs.SetInt("RemoveAds", 0);
        }
    }
    #endregion


    private void Start()
    {
        Debug.Log("Current Gem Countttt: " + PlayerPrefs.GetInt(GEM_COUNT_KEY, 0));

        // Create an AdParam object
        AdParam adParam = new AdParam.Builder().Build();

        // Instantiate an InterstitialAd object
        interstitialAd = new InterstitialAd();

        // Set the ad unit ID
        interstitialAd.AdId = "u8ehe30meh";

        // Load the ad
        interstitialAd.LoadAd(adParam);


    }

    public void loadGame()
    {
        SceneManager.LoadScene("Level " + (levelIndex + 1).ToString());
    }

    //Open(activate) shopping UI
    public void OpenShoppingUI()
    {
        ShoppingUI.gameObject.SetActive(true);
    }

    //Close(deactivate) shopping UI
    public void CloseShoppingUI()
    {
        ShoppingUI.gameObject.SetActive(false);
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

        // Add this code
        // Show the interstitial ad if it is loaded
        

        winUI.gameObject.SetActive(true);


        Vector3 defaultScaleWin = winUI.transform.localScale;

        winUI.transform.localScale = Vector3.one * 0.01f;
        //Debug.Log("defaultScaleWin: " + defaultScaleWin);
        winUI.transform.DOScale(defaultScaleWin, 1f);

        //if RemoveAds has key and value is one, then interstitial ad will not be shown
        if (PlayerPrefs.HasKey("RemoveAds"))
        {
            // Get the value of RemoveAds
            int removeAds = PlayerPrefs.GetInt("RemoveAds");
            // Check if the value is 0 or 1
            if (removeAds == 0)
            {
                if (interstitialAd.Loaded)
                {
                    interstitialAd.Show();
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("RemoveAds", 0);
        }


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

        // Add this code
        // Load a new interstitial ad for the next level
        interstitialAd.LoadAd(new AdParam.Builder().Build());

    }

    public void Next()
    {

        levelIndex = PlayerPrefs.GetInt("LevelIndex");

        levelCounter = PlayerPrefs.GetInt("LevelCounter");
        if (levelCounter == 0)
        {
            levelCounter = 1;
        }

        if (levelIndex < levelCount - 2)
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

        //WinUI deactivate
        //deactivateWinUI();

        //activate menu UI
        //ActivateMenuUI();

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
