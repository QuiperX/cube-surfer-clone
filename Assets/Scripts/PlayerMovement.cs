using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private bool canMotion = false;

    public float VelocityOfPlayer;

    public bool getMotion()
    {
        return canMotion;
    }

    public void StartMotion()
    {
        canMotion = true;
    }

    public void stopMotion()
    {
        canMotion = false;
    }


    private void FixedUpdate()
    {
        //print position of player
        if (!canMotion)
            return;

        movePlayer();

    }

    public void movePlayer()
    {
        transform.position += new Vector3(0f, 0f, 0.1f) * Time.deltaTime * VelocityOfPlayer;
    }

    public void AccessEndPoint()
    {
        //using dotween decrease speed of player smoothly
        DOTween.To(() => VelocityOfPlayer, x => VelocityOfPlayer = x, 0f, 0.6f).OnComplete(() => canMotion = false);
        Debug.Log("AccessEndPoint");
        
    }

    public void Fail()
    {
        canMotion = false;
    }

}
