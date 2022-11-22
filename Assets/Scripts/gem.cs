using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    //gem movement
    private void Update()
    {
        transform.Rotate(0, 0, 45 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2f) * 0.00005f, transform.position.z);
    }
}
