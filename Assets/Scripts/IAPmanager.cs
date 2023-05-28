using HmsPlugin;

using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Utils;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class IAPmanager : MonoBehaviour
{
    // Please insert your products via custom editor. You can find it in Huawei > Kit Settings > IAP tab.

    public static Action<string> IAPLog;

    List<InAppPurchaseData> consumablePurchaseRecord = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeNonConsumables = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeSubscriptions = new List<InAppPurchaseData>();


    //Button for the remove ads
    public Button removeAdsButton;

    //get GemUI to update the gem count
    public GemUIController gemUIController;

    //Result panel
    public GameObject resultPanel;

    //Button for the +100 gems
    public Button gemsButton;


    #region Singleton

    public static IAPmanager Instance { get; private set; }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    #region Monobehaviour

    private void OnEnable()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess += OnBuyProductSuccess;
        HMSIAPManager.Instance.OnInitializeIAPSuccess += OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure += OnInitializeIAPFailure;
        HMSIAPManager.Instance.OnBuyProductFailure += OnBuyProductFailure;
    }

    private void OnDisable()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess -= OnBuyProductSuccess;
        HMSIAPManager.Instance.OnInitializeIAPSuccess -= OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure -= OnInitializeIAPFailure;
        HMSIAPManager.Instance.OnBuyProductFailure -= OnBuyProductFailure;
    }

    void Awake()
    {
        Debug.Log("IAPmanager Awake INSIDE");

        //check if RemoveAds has key and if RemoveAds pref is 1, then remove the button
        if (PlayerPrefs.HasKey("RemoveAds") && PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            removeAdsButton.gameObject.SetActive(false);
        }

        Singleton();
        //Screen.orientation = ScreenOrientation.Landscape;
    }

    void Start()
    {
        // Uncomment below if InitializeOnStart is not enabled in Huawei > Kit Settings > IAP tab.
        
        //HMSIAPManager.Instance.InitializeIAP();
        
        
        //Removeads button listener // purchase product with removeads id PurchaseProduct("removeads");
        removeAdsButton.onClick.AddListener(() => HMSIAPManager.Instance.PurchaseProduct("removeadss"));
        

        //Gems button listener // purchase product with gems id PurchaseProduct("gems");
        gemsButton.onClick.AddListener(() => HMSIAPManager.Instance.PurchaseProduct("gemBoost"));
    }

    #endregion

    //we will create a method for removeAdsButton.onClick.AddListener(() => HMSIAPManager.Instance.PurchaseProduct("removeads"));

    //define test method to test clicker
    public void TestClicker()
    {
        Debug.Log("TestClicker");
    }

    public void InitializeIAP()
    {
        Debug.Log($"InitializeIAP");

        HMSIAPManager.Instance.InitializeIAP();
    }

    private void RestoreProducts()
    {

        HMSIAPManager.Instance.RestorePurchaseRecords((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Consumable)
                {
                    Debug.Log($"Consumable: ProductId {item.ProductId} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} , OrderID  {item.OrderID}");
                    consumablePurchaseRecord.Add(item);
                }
            }
        });

        HMSIAPManager.Instance.RestoreOwnedPurchases((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Subscription)
                {
                    Debug.Log($"Subscription: ProductId {item.ProductId} , ExpirationDate {item.ExpirationDate} , AutoRenewing {item.AutoRenewing} , PurchaseToken {item.PurchaseToken} , OrderID {item.OrderID}");
                    activeSubscriptions.Add(item);
                }

                else if ((IAPProductType)item.Kind == IAPProductType.NonConsumable)
                {
                    Debug.Log($"NonConsumable: ProductId {item.ProductId} , DaysLasted {item.DaysLasted} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} ,OrderID {item.OrderID}");
                    activeNonConsumables.Add(item);
                }
            }
        });

    }

    public void PurchaseProduct(string productID)
    {
        Debug.Log($"PurchaseProduct");
        HMSIAPManager.Instance.PurchaseProduct(productID);
        
    }

    #region Callbacks

    private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        Debug.Log($"OnBuyProductSuccess");


        if (obj.InAppPurchaseData.ProductId == "removeadss")
        {
            IAPLog?.Invoke("Ads Removed!");
            //We will add the code for remove ads here
            //For test

            PlayerPrefs.SetInt("RemoveAds", 1);

            //activate result panel
            resultPanel.SetActive(true);

            //deactivete remove ads button
            removeAdsButton.gameObject.SetActive(false);

        }
        else if (obj.InAppPurchaseData.ProductId == "gemBoost")
        {
            IAPLog?.Invoke("gemBoost +100 Purchased!");

            //We will add the code for gemBoost here - editting gemCount pref
            PlayerPrefs.SetInt("GemCount", PlayerPrefs.GetInt("GemCount") + 100);

            //update the gemUI
            gemUIController.UpdateText();

            
        }

    }

    //close result panel
    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
    }

    private void OnInitializeIAPFailure(HMSException obj)
    {
        IAPLog?.Invoke("IAP is not ready.");
    }

    private void OnInitializeIAPSuccess()
    {
        IAPLog?.Invoke("IAP is ready.");

        RestoreProducts();
    }

    private void OnBuyProductFailure(int code)
    {
        IAPLog?.Invoke("Purchase Fail.");
    }

    #endregion
}