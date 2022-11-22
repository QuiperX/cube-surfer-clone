using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CubeBlockGenerator : MonoBehaviour
{
    //create cube block list
    public List<GameObject> cubeBlockList = new List<GameObject>();

    //total created cube block count
    private int TotalCubeBlockCount = 0;

    public float cubeSize = 0.04f;

    public GameObject CubeObject;

    
    void Start()
    {
        
        CreateCubeBlockStacks();
        Debug.Log("START TIMME!!!");
    }

    // 2 metre aralýklar ile cube block oluþtur
    //random cubeblock creator
    public void CubeBlockStacks(float xPosition, float zPosition)
         
    {
        //Max stack number 5 and min stack number 1
        int stackNumber = Random.Range(2, 5);
        float tempY = 0.02f;
        //stackNumber kadar CubeObject yarat ve bunlarý üst üste koy
        for (int i = 0; i < stackNumber; i++)
        {
            if (i == 0)
            {
                GameObject cubeBlock = Instantiate(CubeObject, new Vector3(xPosition, tempY, zPosition), Quaternion.identity);
                tempY += 0.04f;
                cubeBlockList.Add(cubeBlock);
                TotalCubeBlockCount++;
            }
            else
            {
                GameObject cubeBlock = Instantiate(CubeObject, new Vector3(xPosition, tempY, zPosition), Quaternion.identity);
                tempY += 0.04f;
                cubeBlockList.Add(cubeBlock);
                TotalCubeBlockCount++;
            } 
        }
    }
        

    //Z ekseninde ortalama 2 metre aralýklar ile cube block oluþtur
    public void CreateCubeBlockStacks()
    {
        
        float randomX = Random.Range(-0.145f, 0.145f);
        CubeBlockStacks(randomX, 0.75f);
        
    }


}
