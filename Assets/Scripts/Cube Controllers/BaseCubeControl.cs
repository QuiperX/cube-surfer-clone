using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCubeControl : MonoBehaviour
{
    // Start is called before the first frame update

    private bool collide = false;
    
    public bool GetCollide()
    {
        return collide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AntiCube")
        {
            //GetComponent<BoxCollider>().isTrigger = false;
            GameManager.instance.ActivateLoseUI();
            collide = true;
        }
        
    }
}
