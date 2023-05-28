using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gemCollection : MonoBehaviour
{
    //this script will controll the gem collection of player
    private int gemCount = 0;

    public ParticleSystem gemCollectEffect;
    
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gem"))
        {
            
            gemCollectEffect.Play();
            other.gameObject.SetActive(false);
            gemCount = gemCount + 1;
            //reposition the particle system to gem position and play
            
        }
    }
    
}
