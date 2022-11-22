using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator animatorOfPlayer;

    public PlayerMovement playerMovement;

    

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static PlayerBehaviour Instance;

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    #endregion


    public void VictoryAnimation()
    {
        animatorOfPlayer.SetTrigger("Success");
    }

    public void FailAnimation()
    {
        animatorOfPlayer.SetTrigger("Fail");
    }

    public void StopPlayer()
    {
        DOTween.To(() => playerMovement.VelocityOfPlayer, x => playerMovement.VelocityOfPlayer = x, 0, 0.3f);
    }
}
