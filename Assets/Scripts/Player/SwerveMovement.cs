using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [SerializeField]
    private SwerveInputSystem swerveInputSystem;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private float swerveSpeed = 4f;
    
    [SerializeField]
    private float MaxSwerveAmount = 1f;
    private void Update()
    {
        if (!playerMovement.getMotion())
        {
            return;
        }

        float swerveAmount = swerveInputSystem.MoveFactoryX * swerveSpeed * Time.deltaTime;
        swerveAmount = Mathf.Clamp(swerveAmount, -MaxSwerveAmount, MaxSwerveAmount);

        if (transform.position.x < -0.13f && swerveAmount < 0)
        {
            swerveAmount = 0;
        }
        else if (transform.position.x > 0.13f && swerveAmount > 0)
        {
            swerveAmount = 0;
        }
        transform.position += new Vector3(swerveAmount, 0f, 0f);

    }
}
