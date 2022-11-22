using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    private float _lastFrameFingerPositionX;
    private float _moveFactoryX;
    
    public float MoveFactoryX
    {
        get => _moveFactoryX;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            
            //Debug.Log("Mouse Down");
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactoryX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;

            //Debug.Log("Mouse Drag");
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _moveFactoryX = 0f;

            //Debug.Log("Mouse Up");
        }
    }
}
