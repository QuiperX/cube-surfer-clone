using System.Collections.Generic;
using UnityEngine;
using HmsPlugin;
using UnityEngine.UI;
using HuaweiMobileServices.Ads;

public class AdManager : MonoBehaviour
{
    //private Toggle testAdStatusToggle;
    
    public Button rewardAdButton;

    //public Button hideBannerButton;
    

    public GemUIController gemUIController;

    //add text object to change content

    #region Singleton

    public static AdManager Instance { get; private set; }
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

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnRewardAdClosed = OnRewardAdClosed;

        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.RequestConsentUpdate();

        //testAdStatusToggle = GameObject.FindGameObjectWithTag("Toggle").GetComponent<Toggle>();
        //testAdStatusToggle.isOn = HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds);


        #region SetNonPersonalizedAd , SetRequestLocation

        var builder = HwAds.RequestOptions.ToBuilder();

        builder
            .SetConsent("tcfString")
            .SetNonPersonalizedAd((int)NonPersonalizedAd.ALLOW_ALL)
            .Build();

        bool requestLocation = true;
        var requestOptions = builder.SetConsent("testConsent").SetRequestLocation(requestLocation).Build();

        Debug.Log($"RequestOptions NonPersonalizedAds:  {requestOptions.NonPersonalizedAd}");
        Debug.Log($"Consent: {requestOptions.Consent}");

        #endregion

        //show rewarded ad when button clicked
        rewardAdButton.onClick.AddListener(() =>
        {
            ShowRewardAd();
        });
        

    }

    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        Debug.Log($"[HMS] RewardAdDemoManager OnConsentSuccess consentStatus:{consentStatus} isNeedConsent:{isNeedConsent}");
        foreach (var AdProvider in adProviders)
        {
            Debug.Log($"[HMS] RewardAdDemoManager OnConsentSuccess adproviders: Id:{AdProvider.Id} Name:{AdProvider.Name} PrivacyPolicyUrl:{AdProvider.PrivacyPolicyUrl} ServiceArea:{AdProvider.ServiceArea}");
        }
    }

    private void OnConsentFail(string desc)
    {
        Debug.Log($"[HMS] RewardAdDemoManager OnConsentFail:{desc}");
    }


    public void HideBannerAd()
    {
        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowRewardAd()
    {
        Debug.Log("[HMS] RewardAdDemoManager ShowRewardAd");
        HMSAdsKitManager.Instance.ShowRewardedAd();
        
        
    }


    public void OnRewarded(Reward reward)
    {
        Debug.Log("OMG WE GOT REWARD !!!");
        Debug.Log("[HMS] RewardAdDemoManager rewarded!");

        //Destron button
        rewardAdButton.gameObject.SetActive(false);


        // Increase the GemCount by 5 as a reward
        int gemCount = PlayerPrefs.GetInt("GemCount", 0);
        gemCount += 5;
        PlayerPrefs.SetInt("GemCount", gemCount);

        //Update the GemCount text
        gemUIController.UpdateText();


    }

    public void OnRewardAdClosed()
    {
        Debug.Log("[HMS] RewardAdDemoManager reward ad closed");

    }


}