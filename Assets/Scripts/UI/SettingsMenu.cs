using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Space between menu items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main Button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;


    [Space]
    [Header ("Animation")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;


    [Space]
    [Header ("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

    SettingsMenuItem[] menuItems;
    bool isExpanded = false;
    Button mainButton;

    Vector2 mainButtonPosition;
    int itemsCount;

    public RectTransform arrowImage;
    
    private void Update()
    {
        //Mathf.PingPong(Time.time * 100, 100)
        arrowImage.transform.position = new Vector3(500f + Mathf.PingPong(Time.time * 100, 100), arrowImage.transform.position.y, arrowImage.transform.position.z);
    }
    
    private void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPosition = mainButton.transform.position;

        ResetPositions();
        
    }

    private void ResetPositions()
    {
        for(int i = 0; i< itemsCount; i++)
        {
            menuItems[i].trans.position = mainButtonPosition;
        }
    }
    private void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtonPosition+spacing * (i+1);
                menuItems[i].trans.DOMove(mainButtonPosition+spacing*(i+1), collapseDuration).SetEase(expandEase);
                
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].trans.DOMove(mainButtonPosition, collapseDuration).SetEase(collapseEase);
            }
        }
        mainButton.transform.DORotate(Vector3.forward * 180f, rotationDuration).From(Vector3.zero).SetEase(rotationEase);
    }

    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
