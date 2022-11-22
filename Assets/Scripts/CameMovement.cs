using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CameMovement : MonoBehaviour
{
    //take player object and follow it only z axis
    public GameObject Player;

    //take cubeBlockList from PlayerCubeStackController
    public PlayerCubeStackController playerCubeStackController;

    //get cinemachinevirtualcamera component and field of view
    public Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    //singleton

    #region Singleton
    public static CameMovement instance;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;

    }
    #endregion

    private void FixedUpdate()
    {
        //transform.position = new Vector3(0.213f, 0.373f, Player.transform.position.z - 0.665f);
        transform.position = new Vector3(transform.position.x, transform.position.y, Player.transform.position.z - 0.75f);
        //current camera field of view
        //change priority of cinemachineVirtualCamera

        if (playerCubeStackController.listOfCubeBehaviour.Count < 10)
        {
            DOTween.To(() => cinemachineVirtualCamera.m_Lens.FieldOfView, x => cinemachineVirtualCamera.m_Lens.FieldOfView = x, 50f, 2f);
        }
        else if (playerCubeStackController.listOfCubeBehaviour.Count > 10 && playerCubeStackController.listOfCubeBehaviour.Count < 20)
        {
            DOTween.To(() => cinemachineVirtualCamera.m_Lens.FieldOfView, x => cinemachineVirtualCamera.m_Lens.FieldOfView = x, 65f, 2f);
        }
        else if (playerCubeStackController.listOfCubeBehaviour.Count > 20 && playerCubeStackController.listOfCubeBehaviour.Count < 35)
        {
            DOTween.To(() => cinemachineVirtualCamera.m_Lens.FieldOfView, x => cinemachineVirtualCamera.m_Lens.FieldOfView = x, 85f, 2f);
        }
        else if (playerCubeStackController.listOfCubeBehaviour.Count > 35)
        {
            DOTween.To(() => cinemachineVirtualCamera.m_Lens.FieldOfView, x => cinemachineVirtualCamera.m_Lens.FieldOfView = x, 100f, 2f);
        }
        

    }

    public void FocusPlayer()
    {
        cinemachineVirtualCamera.LookAt = Player.gameObject.transform;
    }
}
